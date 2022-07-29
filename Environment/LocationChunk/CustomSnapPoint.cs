using UnityEngine;

namespace Environment.LocationChunk
{
    public class CustomSnapPoint : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 10f);
        }
    }
}
