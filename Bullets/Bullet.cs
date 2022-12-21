using System;
using Character;
using Common;
using Environment;
using Interfaces;
using ObjectPool;
using Unity.Mathematics;
using UnityEngine;
using Utils;

namespace Bullets
{
    public class Bullet : MonoBehaviour, IDestroyable
    {
        [SerializeField] private GameObject destroyEffect;
        [SerializeField] private BulletType bulletType;
        [SerializeField] protected float startSpeed = 0.1f;
        
        public Vector3 Direction { get; set; }
        public ObjectPoolTags objectTag;

        protected BulletType BulletType { get => bulletType; set => bulletType = value; }
        protected float StartSpeed { get; set; }
        
        protected void Awake()
        {
            if (gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                gameObject.layer = (int)Layers.BulletLayer;
            }
        }

        private void FixedUpdate()
        {
            Moving();
        }

        protected void Moving()
        {
            transform.Translate(Direction.normalized * startSpeed, Space.World);
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((collision.gameObject.layer & (int)Layers.BulletLayer) != 0 
                || (collision.gameObject.layer & (int)Layers.DropLayer) != 0)
            {
                return;
            }
            
            collision.gameObject.HasComponent<PlayerBase>(component =>
            {
                if (component.IsInvulnerable) return;
                
                PlayerBase.OnTakeDamage.Invoke(1);
                GlobalEvents.HealthChanged(component.Health);
                DestroySelf();
            });

            collision.gameObject.HasComponent<Border>(component =>
            {
                if (bulletType != BulletType.Chain)
                    gameObject.SetActive(false);
            });
        }

        public void SetColor(Color color)
        {
            GetComponent<SpriteRenderer>().color = color;
        }

        public void DestroySelf()
        {
            Instantiate(destroyEffect, transform.position, quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
