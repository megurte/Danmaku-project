using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerManager : MonoBehaviour
{
    public SpawnerManagerConfig spawnerManagerConfig;
    public static UnityEvent<int> AwakeSpawner = new UnityEvent<int>();

    private void Start()
    {
        foreach (var enemyWave in spawnerManagerConfig.EnemyWaves)
        {
            StartCoroutine(AwakeSpawnerInTime(enemyWave));
        }
    }

    public static void OnSpawnerAwake(int index)
    {
        AwakeSpawner.Invoke(index);
    }

    public IEnumerator AwakeSpawnerInTime(EnemyWave enemyWave)
    {
        yield return new WaitForSeconds(enemyWave.waveTime); 
        OnSpawnerAwake(enemyWave.spawnerIndex);
    }
}
