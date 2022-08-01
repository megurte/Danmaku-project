using UnityEngine;
using Zenject;

namespace Boss.Camilla
{
    public class CamillaInstaller: MonoInstaller
    {
        [SerializeField] private CamillaSO camillaSettings;
        
        public override void InstallBindings()
        {
            Container.Bind<CamillaSO>().FromInstance(camillaSettings).AsCached().NonLazy();
        }
    }
}