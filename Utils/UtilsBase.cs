using UnityEngine;

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
    }
}