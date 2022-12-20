using System;
using Bullets;
using Factories;
using ObjectPool;
using Unity.Profiling;
using UnityEngine;
using Utils;
using Random = System.Random;

namespace Spells
{
    public class CommonSpells : MonoBehaviour
    {
        private static ProfilerMarker _profilerMarker = new ProfilerMarker(ProfilerCategory.Memory, "RandomShoot");
        
        public static void RandomShooting(CommonSpellSettings settings)
        {
            var seed = Guid.NewGuid().GetHashCode();

            var degree = new Random(seed).Next(0, 360);
            var direction = new Vector2(0, 0);
            var position = new Vector3
            {
                y = settings.CenterPosition.y + Mathf.Cos(degree) * settings.Distance,
                x = settings.CenterPosition.x + Mathf.Sin(degree) * settings.Distance
            };

            direction.y = Mathf.Cos(degree);
            direction.x = Mathf.Sin(degree);

            //factory.SpawnBullet(settings.Bullet, position, direction); TODO: move to factory pattern
            var instObject = ObjectPoolBase.GetBulletFromPool(settings.Bullet.objectTag, position);
            //var instObject = Instantiate(settings.Bullet.gameObject, position, Quaternion.identity);
            instObject.GetComponent<Bullet>().Direction = -direction;
        }
        
        public static void CircleBulletSpawn(SpellSettingsWithCount settings)
        {
            const float angle = 360 * Mathf.Deg2Rad;
            var direction = new Vector2(-1, 1);
            var position = new Vector3();

            for (var i = 1; i <= settings.Count; i++)
            {
                var degree = angle / settings.Count * i;
                position.y = settings.CenterPosition.y + Mathf.Cos(degree) * settings.Distance;
                position.x = settings.CenterPosition.x + Mathf.Sin(degree) * settings.Distance;

                direction.y = Mathf.Cos(degree);
                direction.x = Mathf.Sin(degree);

                var instObject = ObjectPoolBase.GetBulletFromPool(settings.Bullet.objectTag, position);
                instObject.GetComponent<Bullet>().Direction = direction;
            }
        }
        
        public static void TargetPositionShooting(CommonSpellSettingsWithTarget settings)
        {
            var seed = Guid.NewGuid().GetHashCode();
            var rnd = new Random(seed);
            var startPos = settings.CenterPosition;
            var randomXOffset = rnd.NextFloat(0, 2);
            var randomYOffset = rnd.NextFloat(0, 2);
            var newPosition = new Vector3(startPos.x + randomXOffset, startPos.y + randomYOffset, 0);
            var direction = settings.TargetDirection;

            var instObject = ObjectPoolBase.GetBulletFromPool(settings.Bullet.objectTag, newPosition);
            instObject.GetComponent<Bullet>().Direction = direction;
        }
    }
}