using UnityEngine;
using Bullets;
using Random = System.Random;

namespace DefaultNamespace.Spells
{
    public class CommonSpells : MonoBehaviour
    {
        public static void RandomShooting(GameObject bullet, Vector3 centerPos, float distance)
        {
            var degree = new Random().Next(0, 360);
            var direction = new Vector2(0, 0);
            var position = new Vector3();
            
            position.y = centerPos.y + Mathf.Cos(degree) * distance;
            position.x = centerPos.x + Mathf.Sin(degree) * distance;
            
            direction.y = Mathf.Cos(degree);
            direction.x = Mathf.Sin(degree);
            
            var instObject = Instantiate(bullet, position, Quaternion.identity);
            
            instObject.GetComponent<Bullet>().direction = -direction;
        }
        
        public static void CircleBulletSpawn(GameObject bullet, Vector3 centerPos, float distance, int count)
        {
            var direction = new Vector2(-1, 1);
            var angle = 360 * Mathf.Deg2Rad;
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
    }
}