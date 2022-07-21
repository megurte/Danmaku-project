using System;
using Bullets;
using UnityEngine;
using Random = System.Random;

namespace Spells
{
    public class CommonSpells : MonoBehaviour
    {
        public static void RandomShooting(GameObject bullet, Vector3 centerPos, float distance)
        {
            var seed = Guid.NewGuid().GetHashCode();

            var degree = new Random(seed).Next(0, 360);
            var direction = new Vector2(0, 0);
            var position = new Vector3
            {
                y = centerPos.y + Mathf.Cos(degree) * distance,
                x = centerPos.x + Mathf.Sin(degree) * distance
            };

            direction.y = Mathf.Cos(degree);
            direction.x = Mathf.Sin(degree);
            
            var instObject = Instantiate(bullet, position, Quaternion.identity);
            
            instObject.GetComponent<Bullet>().direction = -direction;
        }
        
        public static void CircleBulletSpawn(GameObject bullet, Vector3 centerPos, float distance, int count)
        {
            const float angle = 360 * Mathf.Deg2Rad;
            var direction = new Vector2(-1, 1);
            var position = new Vector3();

            for (var i = 1; i <= count; i++)
            {
                var degree = angle / count * i;
                position.y = centerPos.y + Mathf.Cos(degree) * distance;
                position.x = centerPos.x + Mathf.Sin(degree) * distance;

                direction.y = Mathf.Cos(degree);
                direction.x = Mathf.Sin(degree);

                var instObject = Instantiate(bullet, position, Quaternion.identity);
                instObject.GetComponent<Bullet>().direction = direction;
            }
        }
        
        public static void TargetPositionShooting(GameObject bullet, Vector3 centerPos, float distance, float degree)
        {
            var direction = new Vector2(-1, 1);
            var position = new Vector3();

            position.y = centerPos.y + Mathf.Cos(degree) * distance;
            position.x = centerPos.x + Mathf.Sin(degree) * distance;

            direction.y = Mathf.Cos(degree);
            direction.x = Mathf.Sin(degree);

            var instObject = Instantiate(bullet, position, Quaternion.identity);
            instObject.GetComponent<Bullet>().direction = direction;
        }
    }
}