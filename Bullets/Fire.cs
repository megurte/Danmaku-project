using UnityEngine;

namespace Bullets
{
    public class Fire : Bullet
    {
        private void FixedUpdate()
        {
            Moving();

            if (BulletType != BulletType.Fire) return;
            
            var degree = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
                
            transform.rotation = Quaternion.AngleAxis(degree, Vector3.forward);
        }
    }
}