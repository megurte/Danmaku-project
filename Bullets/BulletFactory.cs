using UnityEngine;
using Utils;

namespace Bullets
{
    public class BulletFactory: MonoBehaviour
    {
        private ObjectPool<Bullet> _bulletPool = new ObjectPool<Bullet>();

        public void SpawnBullet(Bullet bullet, Vector3 position, Vector2 direction)
        {
            var bulletFromPool = _bulletPool.Get(bullet);

            var instObject = Instantiate(bulletFromPool, position, Quaternion.identity);
            instObject.Direction = direction;
        }
    }
}