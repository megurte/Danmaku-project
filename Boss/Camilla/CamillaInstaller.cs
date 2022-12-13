using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Boss.Camilla
{
    public class CamillaInstaller: MonoInstaller
    {
        [SerializeField] private CamillaSO camillaSettings;
        [SerializeField] private CamillaPhaseSettings camillaPhaseSettings;
        [SerializeField] private CamillaBase camillaPrefab;
        [SerializeField] private AudioSource backgroundMusic;
        [SerializeField] private AudioClip bossMusic;

        public override void InstallBindings()
        {
            Container.Bind<CamillaSO>().FromInstance(camillaSettings).AsCached().NonLazy();
            Container.Bind<CamillaPhaseSettings>().FromInstance(camillaPhaseSettings).AsCached().NonLazy();

            StartCoroutine(CamillaInit(3));
        }

        private IEnumerator CamillaInit(float time)
        {
            yield return new WaitForSeconds(time);
            backgroundMusic.clip = bossMusic;
            backgroundMusic.Play();
            camillaPrefab.gameObject.SetActive(true);
            camillaPrefab.BossUI(true);
        }
    }
}