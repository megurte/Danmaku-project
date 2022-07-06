using UnityEngine;

namespace UI
{
    public class ArrowScript: MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Bullet"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}