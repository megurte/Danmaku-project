using System;
using System.Collections;
using System.Xml.Schema;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        public PlayerSO playerSo;

        public int health;
        public int maxHealth;
        public bool isInvulnerable;
        public int exp;
        public int points;
        public int special;
        public float maxValue;
        public int level;
        public float maxLevel;

        private float _playerSpeed;
        private float _specialTimer;
        private float _specialCooldown;
        private GameObject _playerBullet;
        private GameObject _targetBullet;
        private float _targetBulletFrequency;
        private Rigidbody2D _rigidBody;
        private Vector2 _moveVector;
        private float _innerTimer;
        
        private static UnityEvent<DropType, int> OnGetDrop = new UnityEvent<DropType, int>();

        private void Start()
        {
            GetPlayersParamsFromSo();
            
            _innerTimer = _targetBulletFrequency;
            _rigidBody = GetComponent<Rigidbody2D>();
            
            OnGetDrop.AddListener(OnDrop);
        }

        private void FixedUpdate()
        {
            Moving();
            CheckLevelUp();

            if (!isInvulnerable && health <= 0)
                Destroy(gameObject);

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
            _rigidBody.velocity = _moveVector * Time.deltaTime;
        
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W))
                _rigidBody.velocity = new Vector2(0, 0);
        }

        private void CheckLevelUp()
        {
            var keyMap = playerSo.levelUpMap;
            
            if (!keyMap.CheckLength())
                return;

            for (var index = 0; index < keyMap.keys.Count; index++)
            {
                if (keyMap.keys[index] == level)
                    if (index + 1 < keyMap.values.Count)
                        if (exp >= keyMap.values[index + 1])
                            level++;
            }
        }

        private void UseSpecial()
        {
            if (_specialTimer <= 0 && special > 0)
            {
                var settings = playerSo.specialSettings[0];
                
                special--;
                GlobalEventManager.SpecialChanged(special);
                _specialTimer = _specialCooldown;
                _specialTimer -= Time.deltaTime;
                Instantiate(settings.specialGameObject, settings.specialPosition, Quaternion.identity);
                Debug.Log("Special used");
            }
        }

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
                    health += health + value <= maxValue ? value : 0;
                    GlobalEventManager.HealthChanged(health);
                    break;
                case DropType.SpecialDrop:
                    special += special + value <= maxValue ? value : 0;
                    GlobalEventManager.SpecialChanged(special);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, $"Drop index out of range: {type}");
            }
        }

        private void GetPlayersParamsFromSo()
        {
            health = playerSo.health;
            maxHealth = playerSo.maxHealth;
            maxValue = playerSo.maxValue;
            _specialTimer = 0;
            _specialCooldown = playerSo.specialCooldown;
            maxLevel = playerSo.maxLevel;
            special = playerSo.special;
            _playerSpeed = playerSo.speed;
            level = playerSo.level;
            exp = playerSo.exp;
            points = playerSo.points;
            _playerBullet = playerSo.bullet;
            _targetBullet = playerSo.targetBullet;
            _targetBulletFrequency = playerSo.targetBulletFrequency;
        }

        private IEnumerator Invulnerable()
        {
            yield return new WaitForSeconds(2);
            Debug.Log("enter");
            isInvulnerable = false;
        }
    }
}
