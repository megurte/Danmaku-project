using System;
using DefaultNamespace.Spells;
using UnityEngine;

namespace Enemy
{
    public class CommonEnemy : EnemyAbstract
    {
        public EnemySO enemySo;
        public Color bulletColor = default;

        private float _cooldown;
        private GameObject _bullet;
        private int _bulletCount;
        private float _innerTimer;
        private float _speed;

        private void Awake()
        {
            CurrentHp = enemySo.maxHp;
            _cooldown = enemySo.cooldown;
            _bullet = enemySo.bullet;
            _bulletCount = enemySo.counter;
            _speed = enemySo.speed;
            
            OnTakingDamageEvent.AddListener(OnTakingDamage);
        }

        private void FixedUpdate()
        {
            CheckHealth();
            MoveToDirection(GetDirection(targetPosition, transform.position), _speed);
            
            if (_innerTimer > 0)
            {
                _innerTimer -= Time.deltaTime;
            }
            else
            {
                _innerTimer = _cooldown;
                CommonSpells.CircleBulletSpawn(_bullet, transform.position, 1, _bulletCount);
            }
        }

        private void OnTakingDamage(float damage, int enemyID)
        {
            if (enemyID == gameObject.GetInstanceID())
                CurrentHp -= damage;
        }
    }
}
