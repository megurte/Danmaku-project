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
            InjectScriptableObject();
            InjectPhaseSettings();
            
            StartCoroutine(InstantiateCamilla(93));
        }

        private void InjectScriptableObject()
        {
            Container.Bind<CamillaScriptableObject>().FromInstance(camillaSettings).AsCached().NonLazy();
        }

        private void InjectPhaseSettings()
        {
            Container.Bind<CamillaPhaseSettings>().FromInstance(camillaPhaseSettings).AsCached().NonLazy();
        }
        
        private IEnumerator InstantiateCamilla(float time)
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