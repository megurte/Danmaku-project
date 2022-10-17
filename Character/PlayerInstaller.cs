using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Character
{
    public class PlayerInstaller: MonoInstaller
    {
        [SerializeField] private PlayerSO playerSettings;
        [SerializeField] private GameObject characterPrefab;
        [SerializeField] private Transform spawnPoint;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerSO>().FromInstance(playerSettings).AsSingle().NonLazy();

            var playerInstance =
                Container.InstantiatePrefabForComponent<PlayerBase>(characterPrefab, 
                    spawnPoint.position, 
                    Quaternion.identity, 
                    null);
            
            Container.Bind<PlayerBase>().FromInstance(playerInstance).AsSingle().NonLazy();
        }
    }
}