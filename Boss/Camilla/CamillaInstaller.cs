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
        [SerializeField] private GameObject bossTimerUI;
        [SerializeField] private GameObject bossBarUI;

        public override void InstallBindings()
        {
            Container.Bind<CamillaSO>().FromInstance(camillaSettings).AsCached().NonLazy();
            
            StartCoroutine(CamillaInit(85));
        }

        private IEnumerator CamillaInit(float time)
        {
            yield return new WaitForSeconds(time);
            camillaPrefab.SetActive(true);
            bossBarUI.SetActive(true);
            bossTimerUI.SetActive(true);
        }
    }
}