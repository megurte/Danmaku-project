using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Character
{
    public class PlayerInstaller: MonoInstaller
    {
        [SerializeField] private PlayerScriptableObject playerSettings;
        [SerializeField] private PlayerBase characterPrefab;
        [SerializeField] private Transform spawnPoint;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerScriptableObject>().FromInstance(playerSettings).AsSingle().NonLazy();

            var playerInstance =
                Container.InstantiatePrefabForComponent<PlayerBase>(characterPrefab.gameObject, 
                    spawnPoint.position, 
                    Quaternion.identity, 
                    null);
            
            Container.Bind<PlayerBase>().FromInstance(playerInstance).AsSingle().NonLazy();
        }
    }
}