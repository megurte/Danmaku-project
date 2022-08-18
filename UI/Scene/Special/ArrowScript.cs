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
            other.gameObject.HasComponent<IDestroyable>(component => component.DestroySelf());
            other.gameObject.HasComponent<IDamageable>(component => component.TakeDamage(damageToBoss));
        }
    }
}