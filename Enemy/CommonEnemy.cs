using System;
using DefaultNamespace.Spells;
using UnityEngine;

namespace Enemy
{
    public class CommonEnemy : MonoBehaviour
    {
        public Vector2 targetPosition;
        public Color bulletColor = default;
        
        private EnemySO _enemySo;
        private float _cooldown;
        private GameObject _bullet;
        private int _bulletCount;
        private float _innerTimer;
        private float _speed;

        private void Awake()
        {
            _enemySo = GetComponent<EnemyStats>().enemySo;
            _cooldown = _enemySo.cooldown;
            _bullet = _enemySo.bullet;
            _bulletCount = _enemySo.counter;
            _speed = _enemySo.speed;
        }

        private void FixedUpdate()
        {
            MoveToDirection(GetDirection(targetPosition, transform.position));
            
            if (_innerTimer > 0)
            {
                _innerTimer -= Time.deltaTime;
            }
            else
            {
                _innerTimer = _cooldown;
                CommonSpells.CircleBulletSpawn(_bullet, transform.position, 1, _bulletCount);
            }
        }

        private void MoveToDirection(Vector3 direction)
        {
            transform.Translate(direction.normalized * _speed, Space.World);
        }
        
        private Vector3 GetDirection(Vector3 targetPos, Vector3 objectPosition)
        {
            var heading = targetPos - objectPosition;
            var distance = heading.magnitude;

            return heading / distance;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
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
    }
}
