using Bullets;
using Interfaces;
using UnityEngine;
using Utils;

namespace Factories
{
    /*public class BulletFactory: MonoBehaviour, IFactory<Bullet, Vector3, Vector2, Bullet>
    {
        private ObjectPool<Bullet> _bulletPool = new ObjectPool<Bullet>();

        public Bullet Create(Bullet param1, Vector3 param2, Vector2 param3)
        {
            var bulletFromPool = _bulletPool.Get(param1);

            var instantiatedObject = Instantiate(bulletFromPool, param2, Quaternion.identity);
            instantiatedObject.Direction = param3;

            return instantiatedObject;
        }
    }*/
}