using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Spawner
{
    public class SpawnerIterator : MonoBehaviour
    {
        public SpawnerIteratorConfig spawnerIteratorConfig;
        
        public static readonly UnityEvent<int> AwakeSpawner = new UnityEvent<int>();

        private void Start()
        {
            foreach (var enemyWave in spawnerIteratorConfig.enemyWaves)
            {
                StartCoroutine(AwakeSpawnerInTime(enemyWave));
            }
        }

        private static void OnSpawnerAwake(int index)
        {
            AwakeSpawner.Invoke(index);
        }

        private IEnumerator AwakeSpawnerInTime(EnemyWave enemyWave)
        {
            yield return new WaitForSeconds(enemyWave.waveTime); 
            
            OnSpawnerAwake(enemyWave.spawnerIndex);
        }
    }
}
