using System;
using UnityEngine;

namespace Spawner
{
    [Serializable]
    public struct EnemySpawnProperties
    {
        public float spawnTime;
        public GameObject enemyPrefab;
        public Vector2 targetPosition;
        public int enemyNumber;
    }
}