using System.Collections;
using Bullets;
using Spells;
using UnityEngine;
using Utils;
using Random = System.Random;

namespace Enemy
{
    public class CrossEnemy : EnemyBase
    {
        public EnemySO enemySo;

        private GameObject _bulletPrefab;
        private GameObject _drop;
        private float _speed;
        private Vector3 _playersPosition;
        private Vector3 _direction;
        private bool _isCharging = default;
        private bool _isCharged = default;

        private void Awake()
        {
            _bulletPrefab = enemySo.bullet;
            _speed = enemySo.speed;
            CurrentHp = enemySo.maxHp;

            OnTakingDamageEvent.AddListener(OnTakingDamage);
        }

        private void FixedUpdate()
        {

            CheckHealth(enemySo.lootSettings, enemySo.destroyEffect);
            //CommonSpells.RandomShooting(_bulletPrefab, transform.position, 1);
        
            if (!_isCharging)
                MovementToPosition(targetPosition, _speed);


            if (_isCharged)
                MoveToDirection(_direction, _speed);

            if (gameObject.transform.position == targetPosition && !_isCharging)
            {
                _isCharging = true;
                StartCoroutine(Charge());
            }
        }
    
        private void BulletSpawnBeforeDeath()
        {
            var rnd = new Random();

            for (var i = 0; i < 12; i++)
            {
                _playersPosition = UtilsBase.GetNewPlayerPosition();
            
                var position = transform.position;
                var newBullet = Instantiate(_bulletPrefab, new Vector3(
                        position.x + rnd.Next(0, 5),
                        position.y + rnd.Next(0, 3), position.z), 
                    Quaternion.Euler(_playersPosition));

                newBullet.GetComponent<Bullet>().Direction = UtilsBase.GetDirection(_playersPosition, 
                    newBullet.transform.position);
            }
        }

        private IEnumerator Charge()
        {
            yield return new WaitForSeconds(3);
            _speed = 0.5f;
            _isCharged = true;
            targetPosition = UtilsBase.GetNewPlayerPosition();
            _direction = UtilsBase.GetDirection(targetPosition, transform.position);
        }

        public void OnDestroy()
        {
            Instantiate(enemySo.destroyEffect, transform.position, Quaternion.identity);
        }
    }
}
