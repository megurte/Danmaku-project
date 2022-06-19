using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int spawnerIndex = default;
    public SpawnerSO spawnerSo;
    public bool isAwake;

    private List<EnemySpawnProperties> _enemySpawnPropertiesList;
    private float _innerTimer = 0;
    private float _spawnDelta;
    private int _iteration = 0;

    private void Awake()
    {
        _enemySpawnPropertiesList = spawnerSo.enemySpawnPropertiesList;
        SpawnerManager.AwakeSpawner.AddListener(OnSpawnerAwake);
    }

    private void FixedUpdate()
    {
        SpawnCycle();
    }

    private void SpawnEnemy()
    {
        var instObject = Instantiate(spawnerSo.enemyPrefab, transform.position, Quaternion.identity);
        instObject.GetComponent<EnemyAbstract>().targetPosition = _enemySpawnPropertiesList[_iteration].targetPosition;
    }

    private void SpawnCycle()
    {
        if (isAwake)
        {
            Debug.Log("Iteration " + _iteration);
            for (var i = 0; i < _enemySpawnPropertiesList[_iteration].enemyNumber; i++)
            {
                StartCoroutine(Spawn(_innerTimer));

                if (_innerTimer > 0)
                {
                    _innerTimer -= _spawnDelta;
                }
                else
                {
                    isAwake = false;
                }
            }
        }
        else
        {
            _innerTimer = spawnerSo.spawnTime;
        }
    }

    private float SetSpawnDelta()
    {
        return _spawnDelta = _innerTimer / spawnerSo.enemySpawnPropertiesList[_iteration].enemyNumber;
    }

    private void SetIterationData()
    {
        _innerTimer = spawnerSo.spawnTime;
        _spawnDelta = SetSpawnDelta();
    }

    private IEnumerator Spawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SpawnEnemy();
    }

    private void OnSpawnerAwake(int index)
    {
        if (index == spawnerIndex)
        {
            isAwake = true;
            _iteration++;
            SetIterationData();
        }
    }
}
