using Enemy;
using UnityEngine;

namespace Bullets
{
    public class PlayerBullet : MonoBehaviour
    {
        public float startSpeed;
        public Vector2 direction;

        private void FixedUpdate()
        {
            Movement();
        }

        protected void Movement()
        {
            transform.Translate(direction.normalized * startSpeed);
        }
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                EnemyFactory.TakeDamage(1, collision.gameObject.GetInstanceID());
                Destroy(gameObject);
            }
            
            if (collision.CompareTag("Boss"))
            {
                EnemyFactory.TakeDamage(1, collision.gameObject.GetInstanceID());
                Destroy(gameObject);
            }
            
            if (collision.CompareTag("Border"))
                Destroy(gameObject);
        }
    }
}
