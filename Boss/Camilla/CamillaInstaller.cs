using System.Collections;
using UnityEngine;
using Zenject;

namespace Boss.Camilla
{
    public class CamillaInstaller: MonoInstaller
    {
        [SerializeField] private CamillaScriptableObject camillaSettings;
        [SerializeField] private CamillaPhaseSettings camillaPhaseSettings;
        [SerializeField] private CamillaBase camillaPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private AudioSource backgroundMusic;
        [SerializeField] private AudioClip bossMusic;

        public override void InstallBindings()
        {
            Container.Bind<CamillaScriptableObject>().FromInstance(camillaSettings).AsCached().NonLazy();
            Container.Bind<CamillaPhaseSettings>().FromInstance(camillaPhaseSettings).AsCached().NonLazy();
            
            StartCoroutine(CamillaInit(105));
        }

        private IEnumerator CamillaInit(float time)
        {
            yield return new WaitForSeconds(time);
            
            var bossInstance = Container.InstantiatePrefabForComponent<CamillaBase>(camillaPrefab.gameObject, 
                    spawnPoint.position, 
                    Quaternion.identity, 
                    null);
            
            bossInstance.gameObject.SetActive(true);
            camillaPrefab.BossUI(true);
            backgroundMusic.clip = bossMusic;
            backgroundMusic.Play();
        }
    }
}