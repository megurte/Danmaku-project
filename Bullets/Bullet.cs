using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Bullets
{
    public class Bullet : MonoBehaviour
    {
        public float startSpeed = 0.1f;
        public BulletType bulletType;
        public Vector3 direction;
        public float angle = 180;
    
        public enum BulletType
        {
            Other,
            Fireball,
            Fire,
        }
    
        private void FixedUpdate()
        {
            Moving();
        }

        protected void Moving()
        {
            transform.Translate(direction.normalized * startSpeed, Space.World);
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<CharacterController>().health -= 1;
                //collision.GetComponent<Character_controller>().isInvulnerable = true;
                Destroy(gameObject);
            }
            
            if (collision.CompareTag("Border"))
                Destroy(gameObject);
        }
    }
}
