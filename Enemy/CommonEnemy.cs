using System;
using DefaultNamespace.Spells;
using UnityEngine;

namespace Enemy
{
    public class CommonEnemy : EnemyAbstract
    {
        public EnemySO enemySo;


        //public Color bulletColor = default;
        
        private float _cooldown;
        private GameObject _bullet;
        private int _bulletCount;
        private float _innerTimer;
        private float _speed;
        private Spells _spell;
        private GameObject _drop;

        private void Awake()
        {
            CurrentHp = enemySo.maxHp;
            _cooldown = enemySo.cooldown;
            _bullet = enemySo.bullet;
            _bulletCount = enemySo.counter;
            _speed = enemySo.speed;
            _spell = enemySo.spell;
            _drop = enemySo.drop;
            
            OnTakingDamageEvent.AddListener(OnTakingDamage);
        }

        private void FixedUpdate()
        {
            CheckHealth(_drop);
            MoveToDirection(GetDirection(targetPosition, transform.position), _speed);
            
            if (_innerTimer > 0)
            {
                _innerTimer -= Time.deltaTime;
            }
            else
            {
                switch (_spell)
                {
                    case Spells.Circle:
                        CommonSpells.CircleBulletSpawn(_bullet, transform.position, 1, _bulletCount);
                        break;
                    case Spells.RandomShooting:
                        CommonSpells.RandomShooting(_bullet, transform.position, 1);
                        break;
                    case Spells.DirectTarget:
                        CommonSpells.RandomShooting(_bullet, transform.position, 1);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                _innerTimer = _cooldown;
            }
        }

        private void OnTakingDamage(float damage, int enemyID)
        {
            if (enemyID == gameObject.GetInstanceID())
                CurrentHp -= damage;
        }
    }

}
