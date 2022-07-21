using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CharacterController = Character.CharacterController;
using Random = System.Random;

namespace Enemy
{
    public abstract class EnemyFactory: MonoBehaviour
    {
        protected float CurrentHp { get; set; }

        public Vector3 targetPosition;
        
        private float _angle = default;

        private Vector3 _circleCenterPoint = default;

        public static UnityEvent<float, int> OnTakingDamageEvent = new UnityEvent<float, int>();

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
                
                _circleCenterPoint = GetCircleCenter(_circleCenterPoint, radius);
                yield return StartCoroutine(MoveAround(_circleCenterPoint, radius, angularSpeed));
            }
        }
        
        private IEnumerator MoveAround(Vector3 centerPoint, float radius, float angularSpeed)
        {
            while (angularSpeed > 0)
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

        protected Vector3 GetCircleCenter(Vector3 position, float radius)
        {
            return new Vector3(position.x - radius , position.y, position.z);
        }

        protected Vector3 GetNewTargetPosition()
        {
            return GameObject.FindGameObjectWithTag("Player").transform.position;
        }
        
        protected Vector3 GetDirection(Vector3 targetPos, Vector3 objectPosition)
        {
            var heading = targetPos - objectPosition;
            var distance = heading.magnitude;

            return heading / distance;
        }

        protected void CheckHealth(List<LootSettings> lootSettings, GameObject deathEffect)
        {
            if (CurrentHp <= 0)
            {
                DropItems(lootSettings);
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        
        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                CharacterController.TakeDamage(1);
            }

            if (other.CompareTag("Border"))
            {
                Destroy(gameObject);
            }
        }

        private void DropItems(List<LootSettings> lootSettings)
        {
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
                var randomXOffset = rnd.NextFloat(0, 2);
                var randomYOffset = rnd.NextFloat(0, 2);
                var dropPosition = new Vector3(startPos.x + randomXOffset, startPos.y + randomYOffset, 0);
                
                Instantiate(drop, dropPosition, Quaternion.identity);
            }
        }

        public static void TakeDamage(float damage, int enemyID)
        {
            OnTakingDamageEvent.Invoke(damage, enemyID);
        }
    }
}

public static class RandomExtend
{
    public static double NextDouble (this Random @this, double min, double max)
    {
        return @this.NextDouble() * (max - min) + min;
    }

    public static float NextFloat (this Random @this, float min, float max)
    {
        return (float)@this.NextDouble(min, max);
    }
}
