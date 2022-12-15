using System;
using System.Collections;
using Bullets;
using Spells;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Enemy
{
    public class BookEnemy : EnemyBase, IShoot, IDestroyable
    {
        [FormerlySerializedAs("enemySo")] public EnemyScriptableObject enemyScriptableObject;
        private float Cooldown => enemyScriptableObject.cooldown;
        private Bullet Bullet => enemyScriptableObject.bullet;
        private int BulletCount => enemyScriptableObject.counter;
        private float Speed => enemyScriptableObject.speed;
        private Spells Spell => enemyScriptableObject.spell;
        private MoveSet MoveSet => enemyScriptableObject.moveSet;
        private float _innerTimer;
        private Animator _animator;

        [SerializeField] private bool shootingIsAvailable;

        private static readonly int IsCasting = Animator.StringToHash("isCasting");

        private void Awake()
        {
            Factory = gameObject.AddComponent<BulletFactory>();
            CurrentHp = enemyScriptableObject.maxHp;
            _innerTimer = Cooldown;
            _animator = GetComponent<Animator>();
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

            if (Spell == Spells.DirectTarget)
            {
                StartCoroutine(Shoot());
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
                        /*CommonSpells.TargetPositionShooting(new CommonSpellSettingsWithTarget(Bullet, transform.position,
                            1,GetDirection(GetNewTargetPosition(), transform.position)));*/
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                _innerTimer = Cooldown;
            }
        }

        public IEnumerator Shoot()
        {
            if (!shootingIsAvailable) yield break;
            
            _animator.SetBool(IsCasting, false);

            yield return new WaitForSeconds(Cooldown);

            _animator.SetBool(IsCasting, true);
                
            for (var i = 0; i < BulletCount; i++)
            {
                yield return new WaitForSeconds(0.1f);

                var position = transform.position;
                
                CommonSpells.TargetPositionShooting(new CommonSpellSettingsWithTarget(Bullet, position,
                    0.5f, UtilsBase.GetDirection(UtilsBase.GetNewPlayerPosition(), position),
                    0.01f));
            }

            yield return Shoot();
        }
        
        public void DestroySelf()
        {
            Instantiate(enemyScriptableObject.destroyEffect, transform.position, quaternion.identity);
            Destroy(gameObject);
        }
    }
}