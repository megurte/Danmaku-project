using Boss.Camilla;
using Bullets;
using Character;
using Enemy;
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
            other.gameObject.IfHasComponent<IDestroyable>(component => component.DestroySelf());
            other.gameObject.IfHasComponent<IDamageable>(component => 
                EnemyBase.TakeDamage(damageToBoss, other.gameObject.GetInstanceID()));
        }
    }
}