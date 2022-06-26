using System;
using System.Collections;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Events;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        public PlayerSO playerSo;

        public float health;
        public bool isInvulnerable;
        [SerializeField] private int _exp;
        [SerializeField] private int _points;
        [SerializeField] private float _special;
        
        private float _maxValue;
        private float _maxLevel;
        private float _playerSpeed;
        private float _specialTimer;
        private float _specialCooldown;
        [SerializeField] private int _level;
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
                ShootCommon(_level);
            
                if (_level >= 3)
                {
                    ShootTarget(_level);
                    _innerTimer -= Time.deltaTime;
                }
            }

            if (Input.GetKey(KeyCode.X))
                UseSpecial();

            if (_specialTimer - _specialCooldown < 0)
                _specialTimer -= Time.deltaTime;

            if (Input.GetKey(KeyCode.F2))
                _level = 2;
            if (Input.GetKey(KeyCode.F3))
                _level = 3;
            if (Input.GetKey(KeyCode.F4))
                _level = 4;
            
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
                if (keyMap.keys[index] == _level)
                    if (index + 1 < keyMap.values.Count)
                        if (_exp >= keyMap.values[index + 1])
                            _level++;
            }
        }

        private void UseSpecial()
        {
            if (_specialTimer <= 0)
            {
                Debug.Log("Special used");
                _special--;
                _specialTimer = _specialCooldown;
                _specialTimer -= Time.deltaTime;
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
                    if (_level < _maxLevel)
                        _exp += value;
                    else
                        _points += value * 100;
                    break;
                case DropType.PointDrop:
                    _points += value;
                    break;
                case DropType.HealthDrop:
                    health += health + value <= _maxValue ? value : 0;
                    break;
                case DropType.SpecialDrop:
                    _special += _special + value <= _maxValue ? value : 0;
                    break;
            }
        }

        private void GetPlayersParamsFromSo()
        {
            health = playerSo.health;
            _maxValue = playerSo.maxValue;
            _specialTimer = 0;
            _specialCooldown = playerSo.specialCooldown;
            _maxLevel = playerSo.maxLevel;
            _special = playerSo.special;
            _playerSpeed = playerSo.speed;
            _level = playerSo.level;
            _exp = playerSo.exp;
            _points = playerSo.points;
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
