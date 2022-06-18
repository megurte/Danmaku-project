using System;
using DefaultNamespace.Spells;
using UnityEngine;

namespace Enemy
{
    public class CommonEnemy : MonoBehaviour
    {
        private EnemySO _enemySo;
        private float _cooldown;
        private GameObject _bullet;
        private int _bulletCount;
        private float _innerTimer;

        private void Awake()
        {
            _enemySo = GetComponent<EnemyStats>().enemySo;
            _cooldown = _enemySo.cooldown;
            _bullet = _enemySo.bullet;
            _bulletCount = _enemySo.counter;
        }

        void FixedUpdate()
        {
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
    }
}
