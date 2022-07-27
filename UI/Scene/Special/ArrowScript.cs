using Bullets;
using Character;
using Enemy;
using Enviroment;
using Kirin;
using UnityEngine;
using Utils;
using Zenject;

namespace UI.Scene.Special
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
            other.gameObject.HasComponent<KirinModel>(component => 
                EnemyFactory.TakeDamage(40, other.gameObject.GetInstanceID())); // TODO: fix dependency

            other.gameObject.HasComponent<Bullet>(component => Destroy(other.gameObject));
            other.gameObject.HasComponent<EnemyFactory>(component => Destroy(other.gameObject));
        }
    }
}