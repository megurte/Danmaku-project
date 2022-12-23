using System;
using System.Collections;
using Bullets;
using Spells;
using Unity.Mathematics;
using UnityEngine;
using Utils;

namespace Enemy
{
    public class BookEnemy : EnemyBase, IShoot, IDestroyable
    {
        [SerializeField] private EnemyScriptableObject enemyScriptableObject;
        [SerializeField] private bool shootingIsAvailable;
        private float Cooldown => enemyScriptableObject.cooldown;
        private Bullet Bullet => enemyScriptableObject.bullet;
        private int BulletCount => enemyScriptableObject.counter;
        private float Speed => enemyScriptableObject.speed;
        private Spells Spell => enemyScriptableObject.spell;
        private MoveSet MoveSet => enemyScriptableObject.moveSet;
        private float _innerTimer;
        private Animator _animator;

        private static readonly int IsCasting = Animator.StringToHash("isCasting");

        private void Start()
        {
            CurrentHp = enemyScriptableObject.maxHp;
            _innerTimer = Cooldown;
            _animator = GetComponent<Animator>();
            
            switch (MoveSet)
            {
                case MoveSet.MoveAround:
                    StartCoroutine(MoveAroundRoutine(TargetPosition, enemyScriptableObject.radius, Speed, enemyScriptableObject.angularSpeed));
                    break;
                case MoveSet.ToPosition:
                    StartCoroutine(MovementToPosition(TargetPosition, Speed));
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
                var direction = UtilsBase.GetDirection(Player.GetPlayerPosition(), position);
                var spellSettings = new CommonSpellSettingsWithTarget(Bullet, position, 0.5f, direction, 0.01f);
                
                CommonSpells.TargetPositionShooting(spellSettings);
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