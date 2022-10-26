using System;
using System.Collections;
using Bullets;
using Drop;
using Enemy;
using SubEffects;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using Zenject;
using Random = System.Random;

namespace Character
{
    public class PlayerBase : MonoBehaviour
    {
        public PlayerSO playerSo;
        public bool godMod = default;
        public int Health { get; private set; }
        public int Level { get; private set; }
        public int Experience { get; private set; }
        public int Points { get; private set; }
        public bool IsInvulnerable { get; private set; }
        
        private int _maxHealth;
        private float _maxLevel;
        private int _special;
        private float _maxSpecials;
        private float _playerSpeed;
        private bool _slowMode = false;
        private float _specialTimer;
        private float _specialCooldown;
        private GameObject _playerBullet;
        private GameObject _targetBullet;
        private GameObject _destroyEffect;
        private SimpleFlash _flashEffect;
        private float _targetBulletFrequency;
        private Rigidbody2D _rigidBody;
        private Vector2 _moveVector;
        private float _innerTimer;

        public static readonly UnityEvent<int> OnDeath = new UnityEvent<int>();

        private static readonly UnityEvent<int> TranslateCurrentStageScore = new UnityEvent<int>();
        private static readonly UnityEvent<DropType, int> OnGetDrop = new UnityEvent<DropType, int>();
        private static readonly UnityEvent<int> OnTakeDamage = new UnityEvent<int>();

        [Inject]
        public void Construct(PlayerSO playerSettings)
        {
            InitPlayer(playerSettings);
        }
        private void Start()
        {
            _innerTimer = _targetBulletFrequency;
            _flashEffect = GetComponent<SimpleFlash>();
            _rigidBody = GetComponent<Rigidbody2D>();

            if (godMod)
            {
                IsInvulnerable = true;
                Experience = 10000;
            }
            
            OnGetDrop.AddListener(OnDrop);
            OnTakeDamage.AddListener(OnDamage);
            OnDeath.AddListener(PlayerDeath);
            GlobalEvents.OnBossFightFinish.AddListener(OnBossFightFinished);
        }

        private void Update()
        {
            SpeedUpdate();
        }

        // move to Update() method 
        private void FixedUpdate()
        {
            if (Input.anyKey)
                Moving();

            LevelUpdate();

            if (Input.GetKey(KeyCode.Space))
            {
                ShootCommon(Level);
            
                if (Level >= 3)
                {
                    ShootTarget(Level);
                    _innerTimer -= Time.deltaTime;
                }
            }

            if (Input.GetKey(KeyCode.X))
                UseSpecial();
            
            // Test
            if (Input.GetKey(KeyCode.F12))
                Points += 100;

            if (_specialTimer - _specialCooldown < 0)
                _specialTimer -= Time.deltaTime;

            if (Input.GetKey(KeyCode.F2))
                Level = 2;
            if (Input.GetKey(KeyCode.F3))
                Level = 3;
            if (Input.GetKey(KeyCode.F4))
                Level = 4;
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
                _playerSpeed = playerSo.speed;
                _slowMode = false;
            }
        }

        private void LevelUpdate()
        {
            var keyMap = playerSo.levelUpMap;
            
            if (!keyMap.CheckLength())
                return;

            for (var index = 0; index < keyMap.keys.Count; index++)
            {
                if (keyMap.keys[index] == Level)
                {
                    if (index + 1 < keyMap.values.Count)
                    {
                        if (Experience >= keyMap.values[index + 1])
                            Level++;  
                    }
                }
            }
        }

        private void UseSpecial()
        {
            if (!(_specialTimer <= 0) || _special <= 0) return;
            
            var settings = playerSo.specialSettings[0];
                
            _special--;
            _specialTimer = _specialCooldown;
            _specialTimer -= Time.deltaTime;
            
            GlobalEvents.SpecialChanged(_special);

            Instantiate(settings.specialGameObject, settings.specialPosition, Quaternion.identity);
        }

        // TODO: parameters refactor
        private void ShootCommon(int characterLevel)
        {
            switch (characterLevel)
            {
                case 1:
                    CreateShoot(_playerBullet, 0, 0.3f);
                    break;
                case 2:
                    CreateShoot(_playerBullet, 0.3f, 0.3f);
                    CreateShoot(_playerBullet, 0.3f, 0.3f, true);
                    break;
                case 3:
                    CreateShoot(_playerBullet, 0.3f, 0.3f);
                    CreateShoot(_playerBullet, 0.3f, 0.3f, true);
                    break;
                case 4:
                    CreateShoot(_playerBullet, 0.3f, 0.3f);
                    CreateShoot(_playerBullet, 0.3f, 0.3f, true);
                    break;
            }
        }

        private void ShootTarget(int characterLevel)
        {
            if (!(_innerTimer <= 0)) return;

            switch (characterLevel)
            {
                case 3:
                    CreateShoot(_targetBullet, 1.2f, 0.3f);
                    CreateShoot(_targetBullet, 1.2f, 0.3f, true);
                    break;
                case 4:
                    CreateShoot(_targetBullet, 1.2f, 0.3f);
                    CreateShoot(_targetBullet, 1.2f, 0.3f, true);
                    CreateShoot(_targetBullet, 1.8f, 0);
                    CreateShoot(_targetBullet, 1.8f, 0, true);
                    break;
            }
            
            _innerTimer = _targetBulletFrequency;
        }
        
        private void CreateShoot(GameObject prefab, float xOffset, float yOffset, bool deduction = false)
        {
            var position = transform.position;
            
            Vector3 bulletPosition = deduction 
                ? new Vector2(position.x - xOffset, position.y + yOffset) 
                : new Vector2(position.x + xOffset, position.y + yOffset);
            Instantiate(prefab, bulletPosition, Quaternion.identity);
        }

        private void LoseExperienceCrystals()
        {
            var prefab = Resources.Load<GameObject>("Prefab/Drops/expDrop");
            var seed = Guid.NewGuid().GetHashCode();
            var maxVisualCrystals = new Random(seed).Next(6, 12);

            for (var i = 0; i < maxVisualCrystals; i++)
            {
                seed = Guid.NewGuid().GetHashCode();
                
                var rnd = new Random(seed);
                var startPos = transform.position;
                var randomXOffset = rnd.NextFloat(-1, 1);
                var randomYOffset = rnd.NextFloat(-1, 1);
                var dropPosition = new Vector3(startPos.x + randomXOffset, startPos.y + randomYOffset, 0);
                var newObject = Instantiate(prefab, dropPosition, Quaternion.identity);
                
                newObject.gameObject.HasComponent<Collider2D>(component => component.enabled = false);
                newObject.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
                newObject.gameObject.HasComponent<DropBase>(Destroy);
            }
            
            // TODO: fix bug. Next
            // Experience -= 50;
        }
        
        public static void TakeDamage(int damageValue)
        {
            OnTakeDamage.Invoke(damageValue);
        }

        public static void GetDrop(DropType type, int value)
        {
            OnGetDrop.Invoke(type, value);
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
                Health -= damageValue;
                
                _flashEffect.FlashEffect();
                LoseExperienceCrystals();
                StartCoroutine(Invulnerable());
            }

            if (!IsInvulnerable && Health <= 0)
            {
                TranslateCurrentStageScore.Invoke(Points);
                Instantiate(playerSo.destroyEffect, transform.position, Quaternion.identity);
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
            var spawners = GameObject.FindGameObjectsWithTag("Spawner");

            foreach (var spawner in spawners)
                spawner.SetActive(false);

            UtilsBase.ClearBullets<Bullet>();
            UtilsBase.ClearEnemies<EnemyBase>();
            UtilsBase.ClearDrop<DropBase>();
            Destroy(gameObject);
        }
        
        private void InitPlayer(PlayerSO settings)
        {
            playerSo = settings;
            Health = settings.health;
            Level = settings.level;
            Experience = settings.experience;
            Points = settings.points;
            _maxHealth = settings.maxHealth;
            _maxSpecials = settings.maxValue;
            _specialTimer = 0;
            _specialCooldown = settings.specialCooldown;
            _maxLevel = settings.maxLevel;
            _special = settings.special;
            _playerSpeed = settings.speed;
            _playerBullet = settings.bullet;
            _targetBullet = settings.targetBullet;
            _destroyEffect = settings.destroyEffect;
            _targetBulletFrequency = settings.targetBulletFrequency;
        }
    }
}