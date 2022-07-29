using System.Collections;
using UnityEngine;

namespace Environment.LocationChunk
{
    public class ChunkSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject chunkPrefab;
        [SerializeField] private GameObject chunkPool;
        [SerializeField] private float spawnDelay;
        [SerializeField] private bool isAlive;

        private void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            if (isAlive)
            {
                yield return new WaitForSeconds(spawnDelay);

                var newChunk = Instantiate(chunkPrefab, transform.position, Quaternion.identity);

                SnapToOtherChunkPoint(newChunk.transform, transform.position);
                newChunk.transform.parent = chunkPool.transform;
                
                yield return StartCoroutine(SpawnRoutine());
            }
            else
            {
                yield break;
            }
        }
        
        private void SnapToOtherChunkPoint(Transform targetTransform, Vector3 newPosition)
        {
            var bestPosition = newPosition;
            var closestDistance = float.PositiveInfinity;
            var allPoints = FindObjectsOfType<CustomSnapPoint>();
            var targetPoints = targetTransform.GetComponentsInChildren<CustomSnapPoint>();

            foreach (var point in allPoints)
            {
                if (point.transform.parent == targetTransform) continue;

                foreach (var ownPoint in targetPoints)
                {
                    var targetPos = point.transform.position - (ownPoint.transform.position - targetTransform.position);
                    var distance = Vector3.Distance(targetPos, newPosition);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        bestPosition = targetPos;
                    }
                }
            }

            targetTransform.position = closestDistance < 230 ? bestPosition : newPosition;
        }
    }
}
