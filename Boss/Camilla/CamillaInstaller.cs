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
        [SerializeField] private GameObject camillaPrefab;
        [SerializeField] private GameObject bossTimerUI;
        [SerializeField] private GameObject bossBarUI;
        [SerializeField] private AudioSource backgroundMusic;
        [SerializeField] private AudioClip bossMusic;

        public override void InstallBindings()
        {
            Container.Bind<CamillaSO>().FromInstance(camillaSettings).AsCached().NonLazy();
            
            StartCoroutine(CamillaInit(90));
        }

        private IEnumerator CamillaInit(float time)
        {
            yield return new WaitForSeconds(time);
            backgroundMusic.clip = bossMusic;
            backgroundMusic.Play();
            camillaPrefab.SetActive(true);
            camillaPrefab.GetComponent<CamillaBase>().BossUI(true);
        }
    }
}