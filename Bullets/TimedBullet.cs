using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Bullets
{
    public class TimedBullet: Bullet
    {
        [SerializeField]
        private float seconds;
        
        [SerializeField]
        public float acceleratingSpeed;
        
        [SerializeField]
        public float delay;

        private void Start()
        {
            InvokeRepeating(nameof(Accelerate), delay , seconds);
        }

        private void FixedUpdate()
        {
            Moving();
            
            if (bulletType == BulletType.Fire)
            {
                var degree = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(degree, Vector3.forward);
            }
        }

        private new void Moving()
        {
            transform.Translate(direction.normalized * Time.deltaTime * startSpeed, Space.World);
        }

        private void Accelerate()
        {
            startSpeed += acceleratingSpeed;
        }
    }
}