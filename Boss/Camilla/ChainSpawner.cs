using System;
using System.Collections;
using Bullets;
using DefaultNamespace;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;
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
            CamillaPhases.OnChainSpawn.AddListener(OnActionSpawn);
        }

        public void OnActionSpawn(int startIndex, int endIndex)
        {
            for (var spawnerIndex = startIndex; spawnerIndex <= endIndex; spawnerIndex++)
            {
                var rnd = new Random(Guid.NewGuid().GetHashCode());
                var randomDelay = rnd.NextFloat(0, 10);
            
                ActivateChainSpawner(spawnerIndex, randomDelay);
            }
        }
        
        private void ActivateChainSpawner(int spawnerIndex, float randomDelay)
        {
            if (spawnerIndex != index) return;
            StartCoroutine(Spawn(randomDelay));
            StartCoroutine(ClearChains<ChainTarget>());
            StartCoroutine(ClearChains<ChainDirect>());
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
            chain.GetComponent<ChainTarget>().spawnerType = spawnerType;
        }

        private IEnumerator Spawn(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            SpawnChain();
        }

        private static IEnumerator ClearChains<T>() where T : Bullet
        {
            yield return new WaitForSeconds(16);

            UtilsBase.ClearBullets<T>();
        }
    }

    public enum SpawnerType
    {
        Down,
        Up,
    }
}