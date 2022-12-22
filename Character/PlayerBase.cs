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

namespace Character
{
    public class PlayerBase : MonoBehaviour
    {
        public PlayerScriptableObject playerScriptableObject;
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int Points { get; private set; }
        public bool IsInvulnerable { get; private set; }
        public static bool NoDamage = true;

        public static readonly UnityEvent<int> SpecialUsed = new UnityEvent<int>();
        public static readonly UnityEvent<int> OnDeath = new UnityEvent<int>();
        public static readonly UnityEvent<DropType, int> OnGetDrop = new UnityEvent<DropType, int>();
        public static readonly UnityEvent<int> OnTakeDamage = new UnityEvent<int>();
        public static readonly UnityEvent<int> TranslateCurrentStageScore = new UnityEvent<int>();
        public static readonly UnityEvent UpdatePlayerUI = new UnityEvent();

        private bool _godMode = default;
        private PlayerLoseCrystalsService _playerLoseCrystalsService;
        private PlayerSpecials _playerSpecials;
        private float _maxLevel;
        private SimpleFlash _flashEffect;

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
            if (_godMode)
            {
                TurnGodMode();
            }

            UpdatePlayerUI.Invoke();
            OnGetDrop.AddListener(OnDrop);
            OnTakeDamage.AddListener(OnDamage);
            OnDeath.AddListener(PlayerDeath);
            GlobalEvents.OnBossFightFinish.AddListener(OnBossFightFinished);
        }

        private void FixedUpdate()
        {
            LevelAndExperienceUpdate();
            //CheatKeyBinds();
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
            if (_godMode) return;

            _godMode = true;
            IsInvulnerable = true;
            Experience = 10000;
            Debug.LogWarning("God mode is on");
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
                    Health += Health + value <= MaxHealth ? value : 0;
                    GlobalEvents.HealthChanged(Health);
                    break;
                case DropType.SpecialDrop:
                    _playerSpecials.AddSpecial(value);
                    GlobalEvents.SpecialChanged(_playerSpecials.SpecialsCount);
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
            MaxHealth = settings.maxHealth;
            _maxLevel = settings.maxLevel;
            _playerLoseCrystalsService = GetComponent<PlayerLoseCrystalsService>();
            _playerSpecials = GetComponent<PlayerSpecials>();
            _flashEffect = GetComponent<SimpleFlash>();
        }
    }
}