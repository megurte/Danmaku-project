using Enemy;
using Enviroment;
using Kirin;
using UnityEngine;
using Utils;

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
            collision.gameObject.HasComponent<EnemyFactory>(component =>
            {
                EnemyFactory.TakeDamage(1, collision.gameObject.GetInstanceID());
                Destroy(gameObject);
            });

            collision.gameObject.HasComponent<KirinModel>(component =>
            {
                EnemyFactory.TakeDamage(1, collision.gameObject.GetInstanceID());
                Destroy(gameObject);
            });

            collision.gameObject.HasComponent<Border>(component => Destroy(gameObject));
        }
    }
}
