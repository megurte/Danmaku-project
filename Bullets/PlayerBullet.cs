using Enemy;
using Environment;
using UnityEngine;
using Utils;

namespace Bullets
{
    public class PlayerBullet : MonoBehaviour
    {
        public float startSpeed;
        public GameObject destroyEffect;
        public Vector2 direction;

        private const int DamageToEnemy = 1;

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
            collision.gameObject.HasComponent<IDamageable>(component =>
            {
                component.TakeDamage(DamageToEnemy);
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            });

            collision.gameObject.HasComponent<Border>(component => gameObject.SetActive(false));
        }
    }
}
