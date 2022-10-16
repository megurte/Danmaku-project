using System;
using System.Collections;
using System.Collections.Generic;
using Bullets;
using Drop;
using Enemy;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Utils
{
    public static class UtilsBase
    {
        public static Vector3 GetCircleCenter(Vector3 position, float radius)
        {
            return new Vector3(position.x - radius , position.y, position.z);
        }

        public static Vector3 GetNewPlayerPosition()
        {
            return GameObject.FindGameObjectWithTag("Player").transform.position;
        }
        
        public static Vector3 GetDirection(Vector3 targetPos, Vector3 objectPosition)
        {
            var heading = targetPos - objectPosition;
            var distance = heading.magnitude;

            return heading / distance;
        }
        
        public static void ClearBullets<T>() where T : Bullet
        {
            var bullets = Object.FindObjectsOfType<T>();

            foreach (var type in bullets)
            {
                type.gameObject.GetComponent<T>().DestroySelf();
            }
        }
        
        public static void ClearEnemies<T>() where T : EnemyBase
        {
            var enemies = Object.FindObjectsOfType<T>();

            foreach (var type in enemies)
            {
                Object.Destroy(type.gameObject);
            }
        }
        
        public static void ClearDrop<T>() where T : DropBase
        {
            var drops = Object.FindObjectsOfType<T>();

            foreach (var type in drops)
            {
                Object.Destroy(type.gameObject);
            }
        }

        public static void CollectDrop()
        {
            var dropItems = Object.FindObjectsOfType<DropBase>();

            foreach (var item in dropItems)
            {
                item.AttractToPlayer();
            }
        }
    }
}