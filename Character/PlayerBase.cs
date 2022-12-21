using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Bullets;
using Drop;
using Enemy;
using ObjectPool;
using SubEffects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utils;
using Zenject;
using Random = System.Random;

namespace Character
{
    public class PlayerBase : MonoBehaviour
    {
        public PlayerScriptableObject playerScriptableObject;
        public int Health { get; private set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int Points { get; private set; }
        public bool IsInvulnerable { get; private set; }
        public int experienceLoseByDamage = 30;
        public static bool NoDamage = true;

        public static readonly UnityEvent<int> SpecialUsed = new UnityEvent<int>();
        public static readonly UnityEvent<int> OnDeath = new UnityEvent<int>();
        public static readonly UnityEvent<DropType, int> OnGetDrop = new UnityEvent<DropType, int>();
        public static readonly UnityEvent<int> OnTakeDamage = new UnityEvent<int>();
        public static readonly UnityEvent<int> TranslateCurrentStageScore = new UnityEvent<int>();
        public static readonly UnityEvent UpdatePlayerUI = new UnityEvent();
        
        [SerializeField] private bool godMode = default;
        private PlayerLoseCrystalsService _playerLoseCrystalsService;
        private PlayerShootService _playerShootService;
        private float _maxLevel;
        private int _special;
        private float _maxSpecials;
        private float _playerSpeed;
        private bool _slowMode = false;
        private float _specialTimer;
        private float _specialCooldown;
        private SimpleFlash _flashEffect;
        private float _targetBulletFrequency;
        private Vector2 _moveVector;
        private float _innerTimer;
        
        [Inject]
        public void Construct(PlayerScriptableObject playerSettings)
        {
            InitPlayer(playerSettings);
        }
        
        public Vector3 GetPlayerPosition()
        {
            return transform.position;
        }
        
        private void Start()
        {
            if (godMode)
            {
                TurnGodMode();
            }

            UpdatePlayerUI.Invoke();
            OnGetDrop.AddListener(OnDrop);
            OnTakeDamage.AddListener(OnDamage);
            OnDeath.AddListener(PlayerDeath);
            GlobalEvents.OnBossFightFinish.AddListener(OnBossFightFinished);
        }

        private void Update()
        {
            SpeedUpdate();
            
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow) ||
                Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                Moving();
            }
        }

        private void FixedUpdate()
        {
            LevelAndExperienceUpdate();
            CheatKeyBinds();

            if (Input.GetKey(KeyCode.Z))
            {
                _playerShootService.ShootCommon(Level, transform.position);
            
                if (Level >= 3)
                {
                    var value =
                        _playerShootService.ShootTarget(Level, transform.position, _innerTimer, _targetBulletFrequency);
                    _innerTimer = value != 0 ? value : _innerTimer;
                    _innerTimer -= Time.deltaTime;
                }
            }

            if (Input.GetKey(KeyCode.X))
                UseSpecial();

            if (_specialTimer - _specialCooldown < 0)
                _specialTimer -= Time.deltaTime;
        }

        private void CheatKeyBinds()
        {
            if (Input.GetKey(KeyCode.F2))
                Level = 2;
            if (Input.GetKey(KeyCode.F3))
                Level = 3;
            if (Input.GetKey(KeyCode.F4))
                Level = 4;
            if (Input.GetKey(KeyCode.F7))
                TurnGodMode();
        }

        private void TurnGodMode()
        {
            if (godMode) return;

            godMode = true;
            IsInvulnerable = true;
            Experience = 10000;
            Debug.LogWarning("God mode is on");
        }

        private void Moving()
        {
            var moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            
            _moveVector = moveInput.normalized * _playerSpeed;
            transform.Translate(_moveVector);
        }

        private void SpeedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !_slowMode)
            {
                _playerSpeed /= 6;
                _slowMode = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _playerSpeed = playerScriptableObject.speed;
                _slowMode = false;
            }
        }

        private void LevelAndExperienceUpdate()
        {
            var keyMap = playerScriptableObject.levelUpMap;
            
            if (!keyMap.CheckLength())
                return;

            for (var index = 0; index < keyMap.keys.Count; index++)
            {
                if (keyMap.keys[index] != Level) continue;

                if (index + 1 >= keyMap.values.Count) continue;

                if (Experience < keyMap.values[index + 1]) continue;
                
                Level++;
                Experience = 0;
            }
            
            UpdatePlayerUI.Invoke();
        }

        private void UseSpecial()
        {
            if (!(_specialTimer <= 0) || _special <= 0) return;
            
            var settings = playerScriptableObject.specialSettings[0];
                
            _special--;
            _specialTimer = _specialCooldown;
            _specialTimer -= Time.deltaTime;
            
            SpecialUsed.Invoke(_special);
            Instantiate(settings.specialGameObject, settings.specialPosition, Quaternion.identity);
        }

        private void OnDrop(DropType type, int value)
        {
            switch (type)
            {
                case DropType.ExpDrop:
                    if (Level < _maxLevel)
                        Experience += value;
                    else
                        Points += value * 1000;
                    break;
                case DropType.PointDrop:
                    Points += value;
                    break;
                case DropType.HealthDrop:
                    Health += Health + value <= _maxSpecials ? value : 0;
                    GlobalEvents.HealthChanged(Health);
                    break;
                case DropType.SpecialDrop:
                    _special += _special + value <= _maxSpecials ? value : 0;
                    GlobalEvents.SpecialChanged(_special);
                    break;
                default:
                    throw new Exception($"DropType index out of range: {type}");
            }
        }

        private void OnDamage(int damageValue)
        {
            if (Health > 0 && !IsInvulnerable)
            {
                NoDamage = false;
                Health -= damageValue;
                
                _flashEffect.FlashEffect();
                _playerLoseCrystalsService.LoseExperienceCrystals(this);
                LevelAndExperienceUpdate();
                StartCoroutine(Invulnerable());
            }

            if (!IsInvulnerable && Health <= 0)
            {
                Instantiate(playerScriptableObject.destroyEffect, transform.position, Quaternion.identity);
                OnDeath.Invoke(Points);
            }
        }

        private void OnBossFightFinished()
        {
            TranslateCurrentStageScore.Invoke(Points);
        }

        private IEnumerator Invulnerable()
        {
            IsInvulnerable = true;
            
            yield return new WaitForSeconds(2);
            
            IsInvulnerable = false;
        }

        private void PlayerDeath(int score)
        {
            PlayerRunInfo.AddRunScore(Points);
            PlayerRunInfo.SaveScoreData();
            
            //TODO: move to interface ISpawner
            var spawners = GameObject.FindGameObjectsWithTag("Spawner");

            foreach (var spawner in spawners)
                spawner.SetActive(false);

            UtilsBase.ClearBullets<Bullet>();
            //ObjectPoolBase.HideAllActiveBullets();
            UtilsBase.ClearEnemies<EnemyBase>();
            UtilsBase.ClearDrop<DropBase>();
            Destroy(gameObject);
        }
        
        private void InitPlayer(PlayerScriptableObject settings)
        {
            playerScriptableObject = settings;
            Health = settings.health;
            Level = settings.level;
            Experience = settings.experience;
            Points = settings.points;
            _maxSpecials = settings.maxValue;
            _specialTimer = 0;
            _specialCooldown = settings.specialCooldown;
            _maxLevel = settings.maxLevel;
            _special = settings.special;
            _playerSpeed = settings.speed;
            _targetBulletFrequency = settings.targetBulletFrequency;
            _playerLoseCrystalsService = GetComponent<PlayerLoseCrystalsService>();
            _playerShootService = GetComponent<PlayerShootService>();
            _flashEffect = GetComponent<SimpleFlash>();
            _innerTimer = _targetBulletFrequency;
        }
    }
}