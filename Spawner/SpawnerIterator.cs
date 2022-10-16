using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Spawner
{
    public class SpawnerIterator : MonoBehaviour
    {
        public List<GameObject> bookSpawners;

        [SerializeField]
        private SpawnerIteratorConfig spawnerIteratorConfig;
        private GameObject _bookPrefab;
        
        public static readonly UnityEvent<int> AwakeSpawner = new UnityEvent<int>();

        private void Start()
        {
            foreach (var enemyWave in spawnerIteratorConfig.enemyWaves)
            {
                StartCoroutine(AwakeSpawnerInTime(enemyWave));
            }

            StartCoroutine(BookSpawn(15, bookSpawners[0].transform,
                new Vector3(-12.6f, 9.43f, 0)));
            StartCoroutine(BookSpawn(30, bookSpawners[1].transform,
                new Vector3(-12.6f, 9.43f, 0)));
            StartCoroutine(AddsPhaseEnd(80));
        }

        private static void OnSpawnerAwake(int index)
        {
            AwakeSpawner.Invoke(index);
        }

        private static IEnumerator AwakeSpawnerInTime(EnemyWave enemyWave)
        {
            yield return new WaitForSeconds(enemyWave.waveTime); 
            
            OnSpawnerAwake(enemyWave.spawnerIndex);
        }
        
        private IEnumerator BookSpawn(float time, Transform spawner, Vector3 targetPosition)
        {
            yield return new WaitForSeconds(time);

            if (!_bookPrefab)
                _bookPrefab = Resources.Load<GameObject>("Prefab/Enemies/BookEnemy");

            var newInstance = Instantiate(_bookPrefab, spawner.position, Quaternion.identity);
            newInstance.GetComponent<EnemyBase>().SetTargetPosition(targetPosition);
        }
        
        private static IEnumerator AddsPhaseEnd(float time)
        {
            yield return new WaitForSeconds(time); 
            
            UtilsBase.ClearEnemies<EnemyBase>();
            Debug.Log("Adds clear");
        }
    }
}
