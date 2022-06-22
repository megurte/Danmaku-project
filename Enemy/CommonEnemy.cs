using System;
using DefaultNamespace.Spells;
using UnityEngine;

namespace Enemy
{
    public class CommonEnemy : EnemyFactory
    {
        public EnemySO enemySo;
        private float Cooldown => enemySo.cooldown;
        private GameObject Bullet => enemySo.bullet;
        private int BulletCount => enemySo.counter;
        private float Speed => enemySo.speed;
        private Spells Spell => enemySo.spell;
        
        private float _innerTimer;
        
        private void Awake()
        {
            CurrentHp = enemySo.maxHp;
            OnTakingDamageEvent.AddListener(OnTakingDamage);
        }

        private void FixedUpdate()
        {
            CheckHealth(enemySo.lootSettings);
            MoveToDirection(GetDirection(targetPosition, transform.position), Speed);
            
            if (_innerTimer > 0)
            {
                _innerTimer -= Time.deltaTime;
            }
            else
            {
                switch (Spell)
                {
                    case Spells.Circle:
                        CommonSpells.CircleBulletSpawn(Bullet, transform.position, 1, BulletCount);
                        break;
                    case Spells.RandomShooting:
                        CommonSpells.RandomShooting(Bullet, transform.position, 1);
                        break;
                    case Spells.DirectTarget:
                        CommonSpells.RandomShooting(Bullet, transform.position, 1);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                _innerTimer = Cooldown;
            }
        }

        private void OnTakingDamage(float damage, int enemyID)
        {
            if (enemyID == gameObject.GetInstanceID())
                CurrentHp -= damage;
        }
    }
}
