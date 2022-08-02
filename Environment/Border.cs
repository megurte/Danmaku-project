using Bullets;
using UnityEngine;
using Utils;

namespace Environment
{
    public class Border : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.IfHasComponent<Bullet>(component =>
            {
                if (gameObject.GetComponent<ChainTarget>()) return;
                
                // Destroy(collision.gameObject);
            });
        }
    }
}
