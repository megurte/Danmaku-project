using System;
using DefaultNamespace;
using Unity.Mathematics;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Bullets
{
    public class Bullet : MonoBehaviour
    {
        public float startSpeed = 0.1f;
        public BulletType bulletType;
        public GameObject destroyEffect;
        public Vector3 direction;
        public float angle = 180;
    
        public enum BulletType
        {
            Other,
            Fireball,
            Fire,
        }
    
        private void FixedUpdate()
        {
            Moving();
        }

        protected void Moving()
        {
            transform.Translate(direction.normalized * startSpeed, Space.World);
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                CharacterController.TakeDamage(1);
                GlobalEventManager.HealthChanged(collision.GetComponent<CharacterController>().health);
                Destroy(gameObject);
            }
            
            if (collision.CompareTag("Border"))
                Destroy(gameObject);
        }

        public void OnDestroy()
        {
            Instantiate(destroyEffect, transform.position, quaternion.identity);
        }
    }
}
