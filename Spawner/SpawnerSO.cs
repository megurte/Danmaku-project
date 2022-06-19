using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Spawner", menuName = "Spawner")]
public class SpawnerSO : ScriptableObject
{
    public float spawnTime;
    public GameObject enemyPrefab;
    public List<EnemySpawnProperties> enemySpawnPropertiesList;
}

[Serializable]
public struct EnemySpawnProperties
{
    public Vector2 targetPosition;
    public int enemyNumber;
}