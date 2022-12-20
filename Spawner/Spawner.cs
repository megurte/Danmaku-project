using System.Collections;
using System.Collections.Generic;
using Enemy;
using Factories;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Spawner
{
    public class Spawner : MonoBehaviour
    {
        public int spawnerIndex = default;
        [FormerlySerializedAs("spawnerSo")] public SpawnerScriptableObject spawnerScriptableObject;
        public bool isAwake;

        [Inject] private EnemyFactory _enemyFactory;
        private List<EnemySpawnProperties> EnemySpawnList => spawnerScriptableObject.enemySpawnPropertiesList;
        private float _innerTimer = default;
        private float _spawnDelta = default;
        private int _iteration = default;

        private void Awake()
        {
            SetIterationData();
        
            SpawnerIterator.AwakeSpawner.AddListener(OnSpawnerAwake);
        }

        private void FixedUpdate()
        {
            SpawnCycle();
        }

        private void SpawnCycle()
        {
            if (isAwake)
            {
                Debug.Log("Iteration " + _iteration + ". Spawner: " + spawnerIndex);
            
                for (var i = 0; i < EnemySpawnList[_iteration].enemyNumber; i++)
                {
                    StartCoroutine(Spawn(_innerTimer));

                    if (_innerTimer > 0)
                    {
                        _innerTimer -= _spawnDelta;
                    }
                }
            
                _iteration++;
                isAwake = false;
            }
            else
            {
                _iteration = 0;
            }
        }

        private float SetSpawnDelta()
        {
            return _spawnDelta = _innerTimer / spawnerScriptableObject.enemySpawnPropertiesList[_iteration].enemyNumber;
        }

        private void SetIterationData()
        {
            _innerTimer = EnemySpawnList[_iteration].spawnTime;
            _spawnDelta = SetSpawnDelta();
        }

        private IEnumerator Spawn(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            _enemyFactory.Create(EnemySpawnList[_iteration].enemyPrefab, transform.position,
                EnemySpawnList[_iteration].targetPosition);
        }

        private void OnSpawnerAwake(int index)
        {
            if (index == spawnerIndex)
            {
                isAwake = true;
                SetIterationData();
            }
        }
    }
}
