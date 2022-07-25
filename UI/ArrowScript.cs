using Character;
using Enemy;
using Kirin;
using UnityEngine;
using Zenject;

namespace UI
{
    public class ArrowScript: MonoBehaviour
    {
        [SerializeField] private int damageToBoss;

        [Inject]
        public void Construct(PlayerSO settings)
        {
            damageToBoss = settings.specialSettings[0].damageToBoss;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Bullet"))
            {
                if (other.GetComponent<KirinModel>())
                {
                    // TODO: fix dependency
                    // EnemyFactory.TakeDamage(damageToBoss, other.gameObject.GetInstanceID());
                    EnemyFactory.TakeDamage(40, other.gameObject.GetInstanceID());
                }
                else
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
}