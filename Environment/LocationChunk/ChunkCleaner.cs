using UnityEngine;
using Utils;

namespace Environment.LocationChunk
{
    public class ChunkCleaner : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            other.gameObject.HasComponent<CustomSnap>(component => Destroy(other.gameObject));
        }
    }
}
