using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Boss.Camilla
{
    public class CamillaInstaller: MonoInstaller
    {
        [SerializeField] private CamillaSO camillaSettings;
        [SerializeField] private GameObject camillaPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject bossTimerUI;
        [SerializeField] private GameObject bossBarUI;

        public override void InstallBindings()
        {
            Container.Bind<CamillaSO>().FromInstance(camillaSettings).AsCached().NonLazy();
            
            StartCoroutine(CamillaInit());
        }

        private IEnumerator CamillaInit()
        {
            yield return new WaitForSeconds(2);
            camillaPrefab.SetActive(true);
            bossBarUI.SetActive(true);
            bossTimerUI.SetActive(true);
        }
    }
}