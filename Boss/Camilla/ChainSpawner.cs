using System.Collections;
using Bullets;
using UnityEngine;

namespace Boss.Camilla
{
    public class ChainSpawner: MonoBehaviour
    {
        public int index;
        public GameObject chainPrefab;
        public SpawnerType spawnerType;

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

        public IEnumerator Spawn(int spawnerIndex, float delay = 0)
        {
            if (spawnerIndex != index) yield break;

            yield return new WaitForSeconds(delay);
            
            SpawnChain();
        }
    }
}