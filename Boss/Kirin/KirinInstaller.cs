using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Boss.Kirin
{
    public class KirinInstaller : MonoInstaller
    {
        [SerializeField] private KirinSO kirinSettings;
        
        public override void InstallBindings()
        {
            Container.Bind<KirinSO>().FromInstance(kirinSettings).AsCached().NonLazy();
        }
    }
}