﻿using UnityEngine;
using Zenject;

namespace Character
{
    public class PlayerInstaller: MonoInstaller
    {
        [SerializeField] private PlayerScriptableObject playerSettings;
        [SerializeField] private PlayerInputService playerInputService;
        [SerializeField] private PlayerBase characterPrefab;
        [SerializeField] private Transform spawnPoint;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerScriptableObject>().FromInstance(playerSettings).AsSingle().NonLazy();
            Container.Bind<PlayerInputService>().FromInstance(playerInputService).AsSingle().NonLazy();

            var playerInstance =
                Container.InstantiatePrefabForComponent<PlayerBase>(characterPrefab.gameObject, 
                    spawnPoint.position, 
                    Quaternion.identity, 
                    null);
            
            Container.Bind<PlayerBase>().FromInstance(playerInstance).AsSingle().NonLazy();
            SetReference();
        }

        private void SetReference()
        {
            PlayerReference.PlayerBase = characterPrefab;
        }
    }
}