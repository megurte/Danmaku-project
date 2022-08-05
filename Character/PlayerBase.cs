using System;
using System.Collections;
using DefaultNamespace;
using Drop;
using SubEffects;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Character
{
    public class PlayerBase : MonoBehaviour
    {
        public PlayerSO playerSo;
        public int health;
        public int maxHealth;
        public int level;
        public float maxLevel;
        public int exp;
        public int special;
        public float maxSpecials;
        public int points;
        public bool isInvulnerable;

        [SerializeField] private float _playerSpeed;
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
        [SerializeField] private bool _slowMode = false;
        private static UnityEvent<DropType, int> OnGetDrop = new UnityEvent<DropType, int>();
        private static UnityEvent<int> OnTakeDamage = new UnityEvent<int>();

        [Inject]
        public void Construct(PlayerSO playerSettings)
        {
            playerSo = playerSettings;
            
            SetPlayersParametersFromSettings(playerSettings);
        }
        private void Start()
        {
            _innerTimer = _targetBulletFrequency;
            _flashEffect = GetComponent<SimpleFlash>();
            _rigidBody = GetComponent<Rigidbody2D>();
            
            OnGetDrop.AddListener(OnDrop);
            OnTakeDamage.AddListener(OnDamage);
        }

        private void FixedUpdate()
        {
            if (Input.anyKey)
                Moving();

            SpeedUpdate();
            LevelUpdate();

            if (Input.GetKey(KeyCode.Space))
            {
                ShootCommon(level);
            
                if (level >= 3)
                {
                    ShootTarget(level);
                    _innerTimer -= Time.deltaTime;
                }
            }

            if (Input.GetKey(KeyCode.X))
                UseSpecial();

            if (_specialTimer - _specialCooldown < 0)
                _specialTimer -= Time.deltaTime;

            if (Input.GetKey(KeyCode.F2))
                level = 2;
            if (Input.GetKey(KeyCode.F3))
                level = 3;
            if (Input.GetKey(KeyCode.F4))
                level = 4;
        }

        private void Moving()
        {
            var moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            
            _moveVector = moveInput.normalized * _playerSpeed;
            transform.Translate(_moveVector);
        }

        // TODO: Fix
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
                if (keyMap.keys[index] == level)
                {
                    if (index + 1 < keyMap.values.Count)
                    {
                        if (exp >= keyMap.values[index + 1])
                            level++;  
                    }
                }
            }
        }

        private void UseSpecial()
        {
            if (!(_specialTimer <= 0) || special <= 0) return;
            
            var settings = playerSo.specialSettings[0];
                
            special--;
            _specialTimer = _specialCooldown;
            _specialTimer -= Time.deltaTime;
            
            GlobalEvents.SpecialChanged(special);

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
                    if (level < maxLevel)
                        exp += value;
                    else
                        points += value * 100;
                    break;
                case DropType.PointDrop:
                    points += value;
                    break;
                case DropType.HealthDrop:
                    health += health + value <= maxSpecials ? value : 0;
                    GlobalEvents.HealthChanged(health);
                    break;
                case DropType.SpecialDrop:
                    special += special + value <= maxSpecials ? value : 0;
                    GlobalEvents.SpecialChanged(special);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, $"Drop index out of range: {type}");
            }
        }

        private void OnDamage(int damageValue)
        {
            if (health > 0 && !isInvulnerable)
            {
                health -= damageValue;
                
                _flashEffect.FlashEffect();
                StartCoroutine(Invulnerable());
            }

            if (!isInvulnerable && health <= 0)
            {
                Instantiate(playerSo.destroyEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        private IEnumerator Invulnerable()
        {
            var component = GetComponent<CircleCollider2D>();

            isInvulnerable = true;
            component.enabled = false;
            yield return new WaitForSeconds(2);
            component.enabled = true;
            isInvulnerable = false;
        }
        
        private void SetPlayersParametersFromSettings(PlayerSO settings)
        {
            health = settings.health;
            maxHealth = settings.maxHealth;
            maxSpecials = settings.maxValue;
            _specialTimer = 0;
            _specialCooldown = settings.specialCooldown;
            maxLevel = settings.maxLevel;
            special = settings.special;
            _playerSpeed = settings.speed;
            level = settings.level;
            exp = settings.exp;
            points = settings.points;
            _playerBullet = settings.bullet;
            _targetBullet = settings.targetBullet;
            _destroyEffect = settings.destroyEffect;
            _targetBulletFrequency = settings.targetBulletFrequency;
        }
    }
}