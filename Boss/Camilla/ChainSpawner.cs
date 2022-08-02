using System;
using System.Collections;
using Bullets;
using DefaultNamespace;
using UnityEngine;
using Utils;
using Random = System.Random;

namespace Boss.Camilla
{
    public class ChainSpawner: MonoBehaviour
    {
        public int index;
        public GameObject chainPrefab;
        public SpawnerType spawnerType;

        private void Start()
        {
            CamillaPhases.RandomSpawnersActivate.AddListener(RandomSpawnersActivate);
            CamillaPhases.WaveChainsSpawn.AddListener(WaveChainSpawn);
            CamillaPhases.WaveChainsSpawn.AddListener(WaveChainSpawn);
        }

        public void WaveChainSpawn(int startIndex, int endIndex, bool fromLeft = true)
        {
            var delay = 0.2f;
            var start = fromLeft ? startIndex : endIndex;
            var end = fromLeft ? endIndex - 1 : startIndex - 1;

            for (var spawnerIndex = start; spawnerIndex < end; spawnerIndex++)
            {
                ActivateChainSpawner(spawnerIndex, delay);
                delay++;
            }
        }
        
        public void RandomSpawnersActivate(int startIndex, int endIndex)
        {
            for (var spawnerIndex = startIndex; spawnerIndex <= endIndex; spawnerIndex++)
            {
                var rnd = new Random(Guid.NewGuid().GetHashCode());
                var randomDelay = rnd.NextFloat(0, 10);
            
                ActivateChainSpawner(spawnerIndex, randomDelay);
            }
        }
        
        private void ActivateChainSpawner(int spawnerIndex, float delay)
        {
            if (spawnerIndex != index) return;
            StartCoroutine(Spawn(delay));
            // StartCoroutine(ClearChains<ChainBase>());
        }

        private void SpawnChain()
        {
            var position = transform.position;
            Vector3 newPosition;
            Vector3 newRotation;

            if (spawnerType == SpawnerType.Up)
            {
                newPosition = new Vector3(position.x, position.y + 7, position.z);
                newRotation = new Vector3(0, 0, -90);  
            }
            else
            {
                newPosition = new Vector3(position.x, position.y - 7, position.z);
                newRotation = new Vector3(0, 0, 90);
            }

            var chain = Instantiate(chainPrefab, newPosition, Quaternion.Euler(newRotation));
            chain.GetComponent<ChainBase>().spawnerType = spawnerType;
        }

        private IEnumerator Spawn(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            SpawnChain();
        }

        public static IEnumerator ClearChains<T>(float time) where T : Bullet
        {
            yield return new WaitForSeconds(time);

            UtilsBase.ClearBullets<T>();
        }
    }
}