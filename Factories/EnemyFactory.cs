using Enemy;
using UnityEngine;
using Utils;
using Zenject;

namespace Factories
{
    public class EnemyFactory : MonoBehaviour, Interfaces.IFactory<EnemyBase, Vector3, Vector2>
    {
        [Inject] private DiContainer _diContainer;
        
        public EnemyBase Create(EnemyBase prefab, Vector3 spawnPosition, Vector2 targetPosition)
        {
            var instance = _diContainer.InstantiatePrefabAs<EnemyBase>(prefab, spawnPosition);
            
            instance.gameObject.HasComponent<CommonEnemy>(component =>
            {
                if (component.enemyScriptableObject.moveSet != MoveSet.MoveAround)
                {
                    instance.SetTargetPosition(targetPosition);
                }
            });

            return instance;
        }
    }
}
