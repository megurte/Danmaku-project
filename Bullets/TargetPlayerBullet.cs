using UnityEngine;

namespace Bullets
{
    public class TargetPlayerBullet : PlayerBullet
    {
        private GameObject[] _enemies;
        private GameObject _closest;
    
        private void FixedUpdate()
        {
            _enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (_enemies.Length != 0)
            {
                MoveToEnemy();
            }
            else
            {
                Movement();
            }
        }

        private void MoveToEnemy()
        {
            var enemy = FindClosestEnemy();
            direction = GetDirection(enemy.transform.position, transform.position);
            transform.Translate(direction.normalized * startSpeed);
        }
    
        private Vector3 GetDirection(Vector3 targetPos, Vector3 objectPosition)
        {
            var heading = targetPos - objectPosition;
            var distance = heading.magnitude;

            return heading / distance;
        }
        private GameObject FindClosestEnemy()
        {
            var distance = Mathf.Infinity;
            var position = transform.position;

            foreach (var enemy in _enemies)
            {
                var difference = enemy.transform.position - position;
                var currentDistance = difference.sqrMagnitude;

                if (currentDistance < distance)
                {
                    _closest = enemy;
                    distance = currentDistance;
                }
            }

            return _closest;
        }
    }
}
