using System;
using Bullets;
using UnityEngine;
using Random = System.Random;

namespace Spells
{
    public class CommonSpells : MonoBehaviour
    {
        public static void RandomShooting(CommonSpellSettings settings)
        {
            var seed = Guid.NewGuid().GetHashCode();

            var degree = new Random(seed).Next(0, 360);
            var direction = new Vector2(0, 0);
            var position = new Vector3
            {
                y = settings.CenterPos.y + Mathf.Cos(degree) * settings.Distance,
                x = settings.CenterPos.x + Mathf.Sin(degree) * settings.Distance
            };

            direction.y = Mathf.Cos(degree);
            direction.x = Mathf.Sin(degree);
            
            var instObject = Instantiate(settings.Bullet, position, Quaternion.identity);
            
            instObject.GetComponent<Bullet>().direction = -direction;
        }
        
        public static void CircleBulletSpawn(SpellSettingsWithCount settings)
        {
            const float angle = 360 * Mathf.Deg2Rad;
            var direction = new Vector2(-1, 1);
            var position = new Vector3();

            for (var i = 1; i <= settings.Count; i++)
            {
                var degree = angle / settings.Count * i;
                position.y = settings.CenterPos.y + Mathf.Cos(degree) * settings.Distance;
                position.x = settings.CenterPos.x + Mathf.Sin(degree) * settings.Distance;

                direction.y = Mathf.Cos(degree);
                direction.x = Mathf.Sin(degree);

                var instObject = Instantiate(settings.Bullet, position, Quaternion.identity);
                instObject.GetComponent<Bullet>().direction = direction;
            }
        }
        
        public static void TargetPositionShooting(CommonSpellSettingsWithTarget settings)
        {
            var seed = Guid.NewGuid().GetHashCode();
            var rnd = new Random(seed);
            var startPos = settings.CenterPos;
            var randomXOffset = rnd.NextFloat(0, 2);
            var randomYOffset = rnd.NextFloat(0, 2);
            var newPosition = new Vector3(startPos.x + randomXOffset, startPos.y + randomYOffset, 0);
            var direction = settings.TargetDirection;

            var instObject = Instantiate(settings.Bullet, newPosition, Quaternion.identity);
            instObject.GetComponent<Bullet>().direction = direction;
        }
    }

    public class CommonSpellSettings
    {
        public readonly GameObject Bullet;
        public Vector3 CenterPos;
        public readonly float Distance;

        public CommonSpellSettings(GameObject bullet, Vector3 centerPos, float distance)
        {
            Bullet = bullet;
            CenterPos = centerPos;
            Distance = distance;
        }
    }
    
    public class SpellSettingsWithCount: CommonSpellSettings
    {
        public readonly int Count;

        public SpellSettingsWithCount(GameObject bullet, Vector3 centerPos, float distance, int count) 
            : base(bullet, centerPos, distance)
        {
            Count = count;
        }
    }
    
    public class SpellSettingsWithDirectionAndAngle: SpellSettingsWithCount
    {
        public readonly bool RightDirection;
        public readonly float Angle;
        public readonly float Delay;

        public SpellSettingsWithDirectionAndAngle(GameObject bullet, Vector3 centerPos, float distance, int count, bool rightDirection, float angle, float delay) 
            : base(bullet, centerPos, distance, count)
        {
            RightDirection = rightDirection;
            Angle = angle;
            Delay = delay;
        }
    }
    
    public class CommonSpellSettingsWithTarget: CommonSpellSettings
    {
        public readonly Vector3 TargetDirection;

        public CommonSpellSettingsWithTarget(GameObject bullet, Vector3 centerPos, float distance, Vector3 targetDirection) 
            : base(bullet, centerPos, distance)
        {
            TargetDirection = targetDirection;
        }
    }
}