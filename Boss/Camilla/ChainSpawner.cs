using System;
using System.Collections;
using System.Collections.Generic;
using Bullets;
using Enemy;
using UnityEngine;
using Random = System.Random;

namespace Boss.Camilla
{
    public class ChainSpawner: MonoBehaviour
    {
        public GameObject chainPrefab;
        
        private void Start()
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            var randomDelay = rnd.NextFloat(0, 10);
            
            StartCoroutine(Spawn(randomDelay));
            StartCoroutine(ClearChains());
        }

        private void SpawnChain()
        {
            var position = transform.position;
            var newPosition = new Vector3(position.x, position.y - 7, position.z);
            var chain = Instantiate(chainPrefab, newPosition, Quaternion.identity);
            
            chain.transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
        }

        private IEnumerator Spawn(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            SpawnChain();
        }
        
        public IEnumerator ClearChains()
        {
            yield return new WaitForSeconds(16);

            var chains = FindObjectsOfType<Chain>();

            foreach (var type in chains)
            {
                Destroy(type.gameObject);
            }
        }
    }
}