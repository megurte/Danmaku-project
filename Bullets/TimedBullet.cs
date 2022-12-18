using Interfaces;
using UnityEngine;

namespace Bullets
{
    public class TimedBullet: Bullet, IPooledType
    {
        [SerializeField] private float seconds;
        [SerializeField] private float acceleratingSpeed;
        [SerializeField] private float delay;

        private const float TrueStartSpeed = 0.1f;

        public void OnObjectPooled()
        {
            StartSpeed = TrueStartSpeed;
            InvokeRepeating(nameof(Accelerate), delay , seconds);
        }

        private void FixedUpdate()
        {
            Moving();
            
            if (BulletType != BulletType.Fire) return;
            
            var degree = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
            
            transform.rotation = Quaternion.AngleAxis(degree, Vector3.forward);
        }

        private new void Moving()
        {
            transform.Translate(Direction.normalized * (Time.deltaTime * StartSpeed), Space.World);
        }

        private void Accelerate()
        {
            StartSpeed += acceleratingSpeed;
        }
    }
}