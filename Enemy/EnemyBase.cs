using System;
using System.Collections;
using System.Collections.Generic;
using Bullets;
using Character;
using Common;
using Drop;
using Environment;
using Factories;
using UnityEngine;
using Utils;
using Zenject;
using Random = System.Random;

namespace Enemy
{
    public abstract class EnemyBase: MonoBehaviour, IDamageable
    {
        protected float CurrentHp { get; set; }
        protected Vector3 TargetPosition;

        [Inject] private DropFactory _dropFactory;
        [Inject] protected PlayerBase Player;
        private float _angle = default;
        private Vector3 _circleCenterPoint = default;

        protected IEnumerator MovementToPosition(Vector3 targetPos, float speed)
        {
            while (transform.position != targetPos) 
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                yield return null;
            }
        }
    
        protected void MoveToDirection(Vector3 direction, float speed)
        {
            transform.Translate(direction.normalized * speed, Space.World);
        }

        protected IEnumerator MoveAroundRoutine(Vector3 targetPos, float radius, float speed, float angularSpeed)
        {
            while (transform.position != targetPos) 
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                yield return null;
            }

            if (transform.position == targetPos)
            {
                _circleCenterPoint = transform.position;
                
                _circleCenterPoint = UtilsBase.GetCircleCenter(_circleCenterPoint, radius);
                yield return StartCoroutine(MoveAround(_circleCenterPoint, radius, angularSpeed));
            }
        }
        
        private IEnumerator MoveAround(Vector3 centerPoint, float radius, float angularSpeed)
        {
            while (true)
            {
                var positionX = centerPoint.x + Mathf.Cos(_angle) * radius;
                var positionY = centerPoint.y + Mathf.Sin(_angle) * radius;

                transform.position = new Vector2(positionX, positionY);
                _angle += Time.deltaTime * angularSpeed;

                if (_angle >= 360f)
                    _angle = 0;
                
                yield return null;
            }
        }

        protected void CheckHealth(List<LootSettings> lootSettings = null, GameObject deathEffect = null)
        {
            if (CurrentHp > 0) return;
            
            if (deathEffect != null)
                Instantiate(deathEffect, transform.position, Quaternion.identity);

            gameObject.HasComponent<Barrier>(component => component.SwitchIsMagicBarrierActive(false));
            
            DropItems(lootSettings);
            Destroy(gameObject);
        }
        
        protected void OnTriggerEnter2D(Collider2D other)
        {
            other.gameObject.HasComponent<PlayerBase>(component =>
            {
                if (component.IsInvulnerable) return;
                
                PlayerBase.OnTakeDamage.Invoke(1);
                GlobalEvents.HealthChanged(component.Health);
            });
            other.gameObject.HasComponent<Border>(component => Destroy(gameObject));
        }

        protected void DropItems(List<LootSettings> lootSettings)
        {
            if (lootSettings == null) return;

            foreach (var loot in lootSettings)
            {
                for (var i = 0; i < loot.dropNumber; i++)
                {
                    var seed = Guid.NewGuid().GetHashCode();
                    
                    DropSpawn(loot.dropItem, loot.chance, seed);
                }
            }
        }

        private void DropSpawn(DropBase drop, float chance, int seed)
        {
            if (new Random(seed).NextFloat(0, 1) <= chance)
            {
                _dropFactory.Create(drop, transform.position, seed);
            }
        }

        public void TakeDamage(float damage)
        {
            CurrentHp -= damage;
        }

        public void SetTargetPosition(Vector3 newPosition)
        {
            TargetPosition = newPosition;
        }
    }
}