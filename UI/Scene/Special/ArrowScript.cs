using Bullets;
using Character;
using Enemy;
using Environment;
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
            other.gameObject.IfHasComponent<KirinBase>(component => 
                EnemyBase.TakeDamage(40, other.gameObject.GetInstanceID())); // TODO: fix dependency
            other.gameObject.IfHasComponent<Bullet>(component => Destroy(other.gameObject));
            other.gameObject.IfHasComponent<CommonEnemy>(component => Destroy(other.gameObject));
        }
    }
}