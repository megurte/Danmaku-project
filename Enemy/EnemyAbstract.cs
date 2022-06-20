using System;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public abstract class EnemyAbstract: MonoBehaviour
    {
        protected float CurrentHp { get; set; }
        
        public Vector3 targetPosition;
        public static UnityEvent<float, int> OnTakingDamageEvent = new UnityEvent<float, int>();
        
        protected void MovementToPosition(Vector3 targetPos, float speed)
        {    
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);   
        }
    
        protected void MoveToDirection(Vector3 direction, float speed)
        {
            transform.Translate(direction.normalized * speed, Space.World);
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

        protected void CheckHealth()
        {
            if (CurrentHp <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<CharacterController>().health -= 1;
                //collision.GetComponent<Character_controller>().isInvulnerable = true;
            }

            if (other.CompareTag("Border"))
            {
                Destroy(gameObject);
            }
        }

        public static void TakeDamage(float damage, int enemyID)
        {
            OnTakingDamageEvent.Invoke(damage, enemyID);
        }
    }
}
