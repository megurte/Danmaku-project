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
            
            for (var index = 0; index < keyMap.keys.Count; index++)
            {
                if (keyMap.keys[index] == _level)
                    if (index + 1 < keyMap.values.Count)
                        if (_exp >= keyMap.values[index + 1])
                            _level++;
            }
        }

        private void ShootCommon(int characterLevel)
        {
            Vector2 bulletPosition1;
            Vector2 bulletPosition2;
            Vector2 bulletPosition3;
            Vector2 bulletPosition4;

            var positionX = transform.position.x;
            var positionY = transform.position.y;

            switch (characterLevel)
            {
                case 1:
                    bulletPosition1 = new Vector2(positionX, transform.position.y + 0.3f);
                    Instantiate(_playerBullet, bulletPosition1, Quaternion.identity);
                    break;
                case 2:
                    bulletPosition1 = new Vector2(positionX + 0.3f, positionY + 0.3f);
                    bulletPosition2 = new Vector2(positionX - 0.3f, positionY + 0.3f);
                    Instantiate(_playerBullet, bulletPosition1, Quaternion.identity);
                    Instantiate(_playerBullet, bulletPosition2, Quaternion.identity);
                    break;
                case 3:
                    bulletPosition1 = new Vector2(positionX + 0.3f, positionY + 0.3f);
                    bulletPosition2 = new Vector2(positionX - 0.3f, positionY + 0.3f);
                    Instantiate(_playerBullet, bulletPosition1, Quaternion.identity);
                    Instantiate(_playerBullet, bulletPosition2, Quaternion.identity);

                    break;
                case 4:
                    bulletPosition1 = new Vector2(transform.position.x + 0.3f, positionY + 0.3f);
                    bulletPosition2 = new Vector2(positionX - 0.3f, positionY + 0.3f);
                    /*bulletPosition3 = new Vector2(positionX + 0.6f, positionY + 0.3f);
                bulletPosition4 = new Vector2(positionX - 0.6f, positionY + 0.3f);*/
                    Instantiate(_playerBullet, bulletPosition1, Quaternion.identity);
                    Instantiate(_playerBullet, bulletPosition2, Quaternion.identity);
                    /*Instantiate(bullet, bulletPosition3, Quaternion.identity);
                Instantiate(bullet, bulletPosition4, Quaternion.identity);*/
                    break;
            }
        }

        private void ShootTarget(int characterLevel)
        {
            if (!(_innerTimer <= 0)) return;
        
            Vector2 targetBulletPosition1;
            Vector2 targetBulletPosition2;
            Vector2 targetBulletPosition3;
            Vector2 targetBulletPosition4;
        
            var positionX = transform.position.x;
            var positionY = transform.position.y;
        
            switch (characterLevel)
            {
                case 3:
                    targetBulletPosition1 = new Vector2(positionX + 1.2f, positionY + 0.3f);
                    targetBulletPosition2 = new Vector2(positionX - 1.2f, positionY + 0.3f);
                    Instantiate(_targetBullet, targetBulletPosition1, Quaternion.identity);
                    Instantiate(_targetBullet, targetBulletPosition2, Quaternion.identity);
                    break;
                case 4:
                    targetBulletPosition1 = new Vector2(positionX + 1.2f, positionY + 0.3f);
                    targetBulletPosition2 = new Vector2(positionX - 1.2f, positionY + 0.3f);
                    targetBulletPosition3 = new Vector2(positionX + 1.8f, positionY + 0.0f);
                    targetBulletPosition4 = new Vector2(positionX - 1.8f, positionY + 0.0f);
                    Instantiate(_targetBullet, targetBulletPosition1, Quaternion.identity);
                    Instantiate(_targetBullet, targetBulletPosition2, Quaternion.identity);
                    Instantiate(_targetBullet, targetBulletPosition3, Quaternion.identity);
                    Instantiate(_targetBullet, targetBulletPosition4, Quaternion.identity);
                    break;
            }
            
            _innerTimer = _targetBulletFrequency;
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
            _maxValue = playerSo.maxLevel;
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
