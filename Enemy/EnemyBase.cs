using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using Environment;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using Random = System.Random;

namespace Enemy
{
    public abstract class EnemyBase: MonoBehaviour, IDamageable
    {
        protected float CurrentHp { get; set; }

        public Vector3 targetPosition;
        
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
            if (CurrentHp <= 0)
            {
                if (deathEffect != null)
                    Instantiate(deathEffect, transform.position, Quaternion.identity);
                
                DropItems(lootSettings);
                Destroy(gameObject);
            }
        }
        
        protected void OnTriggerEnter2D(Collider2D other)
        {
            other.gameObject.HasComponent<PlayerBase>(component =>
            {
                if (component.isInvulnerable) return;
                
                PlayerBase.TakeDamage(1);
                GlobalEvents.HealthChanged(component.health);
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

        private void DropSpawn(GameObject drop, float chance, int seed)
        {
            if (new Random(seed).NextFloat(0, 1) <= chance)
            {
                var rnd = new Random(seed);
                var startPos = transform.position;
                var randomXOffset = rnd.NextFloat(-1, 1);
                var randomYOffset = rnd.NextFloat(-1, 1);
                var dropPosition = new Vector3(startPos.x + randomXOffset, startPos.y + randomYOffset, 0);
                
                Instantiate(drop, dropPosition, Quaternion.identity);
            }
        }

        public void TakeDamage(float damage)
        {
            CurrentHp -= damage;
        }
    }
}