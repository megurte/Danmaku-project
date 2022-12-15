using System;
using System.Collections;
using System.Collections.Generic;
using Bullets;
using Spells;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Enemy
{
    public class CommonEnemy : EnemyBase, IDestroyable
    {
        [FormerlySerializedAs("enemySo")] public EnemyScriptableObject enemyScriptableObject;
        private float Cooldown => enemyScriptableObject.cooldown;
        private Bullet Bullet => enemyScriptableObject.bullet;
        private int BulletCount => enemyScriptableObject.counter;
        private float Speed => enemyScriptableObject.speed;
        private Spells Spell => enemyScriptableObject.spell;
        private MoveSet MoveSet => enemyScriptableObject.moveSet;
        private float _innerTimer;
        
        private void Awake()
        {
            Factory = gameObject.AddComponent<BulletFactory>();
            CurrentHp = enemyScriptableObject.maxHp;
            _innerTimer = Cooldown;
        }

        private void Start()
        {
            switch (MoveSet)
            {
                case MoveSet.MoveAround:
                    StartCoroutine(MoveAroundRoutine(TargetPosition, enemyScriptableObject.radius, Speed, enemyScriptableObject.angularSpeed));
                    break;
                case MoveSet.ToPosition:
                    StartCoroutine(MovementToPosition(TargetPosition, Speed));
                    break;
                case MoveSet.ToPoint:
                    //
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void FixedUpdate()
        {
            CheckHealth(enemyScriptableObject.lootSettings, enemyScriptableObject.destroyEffect);

            if (_innerTimer > 0)
            {
                _innerTimer -= Time.deltaTime;
            }
            else
            {
                switch (Spell)
                {
                    case Spells.Circle:
                        CommonSpells.CircleBulletSpawn(new SpellSettingsWithCount(Bullet, transform.position, 1, BulletCount));
                        break;
                    case Spells.RandomShooting:
                        CommonSpells.RandomShooting(new CommonSpellSettings(Bullet, transform.position, 1), Factory);
                        break;
                    case Spells.DirectTarget:
                        CommonSpells.TargetPositionShooting(new CommonSpellSettingsWithTarget(Bullet, transform.position,
                            1, UtilsBase.GetDirection(UtilsBase.GetNewPlayerPosition(), transform.position), 0.01f));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                _innerTimer = Cooldown;
            }
        }

        public void DestroySelf()
        {
            Instantiate(enemyScriptableObject.destroyEffect, transform.position, quaternion.identity);
            Destroy(gameObject);
        }
    }
}
