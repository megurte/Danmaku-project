using Kirin;
using UnityEngine;

namespace Bullets
{
    public class PlayerBullet : MonoBehaviour
    {
        public float startSpeed;
        public Vector2 direction;

        private void FixedUpdate()
        {
            transform.Translate(direction.normalized * startSpeed);
        }
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<KirinStats>().CurrentHp -= 1;
                Destroy(gameObject);
            }
        
            if (collision.CompareTag("Border"))
                Destroy(gameObject);
        }
    }
}
