using UnityEngine;
using Zenject;

namespace Factories
{
    public class FactoryInstaller : MonoInstaller
    {
        [SerializeField] private EnemyFactory enemyFactory;
        [SerializeField] private DropFactory dropFactory;

        public override void InstallBindings()
        {
            Container.Bind<EnemyFactory>().FromInstance(enemyFactory).AsSingle().NonLazy();
            Container.Bind<DropFactory>().FromInstance(dropFactory).AsCached();
        }
    }
}