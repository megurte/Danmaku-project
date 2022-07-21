using UnityEngine;

namespace Bullets
{
    public class Fire : Bullet
    {
        private void FixedUpdate()
        {
            Moving();

            if (bulletType != BulletType.Fire) return;
            
            var degree = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                
            transform.rotation = Quaternion.AngleAxis(degree, Vector3.forward);
        }
    }
}