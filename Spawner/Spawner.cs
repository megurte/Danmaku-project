using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Spawner
{
    public class Spawner : MonoBehaviour
    {
        public int spawnerIndex = default;
    
        public SpawnerSO spawnerSo;
    
        public bool isAwake;

        private List<EnemySpawnProperties> EnemySpawnPropertiesList => spawnerSo.enemySpawnPropertiesList;
    
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

        private void SpawnEnemy()
        {
            var instObject = Instantiate(EnemySpawnPropertiesList[_iteration].enemyPrefab,
                transform.position, Quaternion.identity);

            if (instObject.GetComponent<CommonEnemy>().enemySo.moveSet != MoveSet.MoveAround)
            {
                instObject.GetComponent<EnemyBase>().targetPosition 
                    = EnemySpawnPropertiesList[_iteration].targetPosition;
            }
        }

        private void SpawnCycle()
        {
            if (!isAwake) return;
            
            Debug.Log("Iteration " + _iteration + ". Spawner: " + spawnerIndex);
            
            for (var i = 0; i < EnemySpawnPropertiesList[_iteration].enemyNumber; i++)
            {
                StartCoroutine(Spawn(_innerTimer));

                if (_innerTimer > 0)
                    _innerTimer -= _spawnDelta;
            }
            
            _iteration++;
            isAwake = false;
        }

        private float SetSpawnDelta()
        {
            return _spawnDelta = _innerTimer / spawnerSo.enemySpawnPropertiesList[_iteration].enemyNumber;
        }

        private void SetIterationData()
        {
            _innerTimer = EnemySpawnPropertiesList[_iteration].spawnTime;
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
                SetIterationData();
            }
        }
    }
}
