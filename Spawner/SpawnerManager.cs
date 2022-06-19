using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerManager : MonoBehaviour
{
    public EnemySO spawnerManagerConfig;
    public static UnityEvent<int> AwakeSpawner = new UnityEvent<int>();

    private void Start()
    {
        StartCoroutine(Test(10));
    }

    public static void OnSpawnerAwake(int index)
    {
        AwakeSpawner.Invoke(index);
    }

    public IEnumerator Test(int index)
    {
        yield return new WaitForSeconds(4); 
        OnSpawnerAwake(index);
    }
}
