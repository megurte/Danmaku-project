using Common;
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

        protected void Awake()
        {
            if (gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                gameObject.layer = (int)Layers.BulletLayer;
            }
        }
        
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
            if ((collision.gameObject.layer & (int)Layers.BulletLayer) != 0 
                || (collision.gameObject.layer & (int)Layers.DropLayer) != 0)
            {
                return;
            }
            
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
