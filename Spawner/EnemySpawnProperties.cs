using System;
using Enemy;
using UnityEngine;

namespace Spawner
{
    [Serializable]
    public struct EnemySpawnProperties
    {
        public float spawnTime;
        public EnemyBase enemyPrefab;
        public Vector2 targetPosition;
        public int enemyNumber;
    }
}