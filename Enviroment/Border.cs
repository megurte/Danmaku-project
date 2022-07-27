using Bullets;
using UnityEngine;
using Utils;

namespace Enviroment
{
    public class Border : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.HasComponent<Bullet>(component => Destroy(collision.gameObject));
        }
    }
}
