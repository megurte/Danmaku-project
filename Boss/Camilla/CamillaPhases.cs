using System.Collections;
using Bullets;
using DefaultNamespace;
using Spells;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using Zenject;

namespace Boss.Camilla
{
    public class CamillaPhases : MonoBehaviour
    {
        public static readonly UnityEvent<int, int> AllRandomSpawnersActivate = new UnityEvent<int, int>();
        public static readonly UnityEvent<int, int, int> RandomSpawnersActivate = new UnityEvent<int, int, int>();
        public static readonly UnityEvent<int, int, bool> WaveChainsSpawn = new UnityEvent<int, int, bool>();
        public static readonly UnityEvent<SpellSettingsWithCount> CircleBulletWithRandomColorsSpawn = new UnityEvent<SpellSettingsWithCount>();
        
        private CamillaSO _camillaSettings;

        [Inject] 
        public void Construct(CamillaSO settings)
        {
            _camillaSettings = settings;
        }
        
        private const int StartIndexDown = 1;
        private const int EndIndexDown = 14;
        private const int StartIndexUp = 15;
        private const int EndIndexUp = 25;

        public IEnumerator InitPhaseOne()
        {
            GlobalEvents.OnPhaseChange.AddListener(OnPhaseChange);
            Debug.Log("Init " + Phases.PhaseOne);


            yield return new WaitForSeconds(2);
            CircleBulletWithRandomColorsSpawn.Invoke(new SpellSettingsWithCount(_camillaSettings.bullets[4],transform.position,2,70));

            yield return new WaitForSeconds(1);
            CircleBulletWithRandomColorsSpawn.Invoke(new SpellSettingsWithCount(_camillaSettings.bullets[0],transform.position,2,70));

            yield return new WaitForSeconds(1);
            CircleBulletWithRandomColorsSpawn.Invoke(new SpellSettingsWithCount(_camillaSettings.bullets[4],transform.position,2,70));

            UtilsBase.ClearBullets<ChainBase>();

            
            yield return new WaitForSeconds(14);
            UtilsBase.ClearBullets<ChainBase>();
            
            //WaveChainsSpawn.Invoke(StartIndexUp, EndIndexUp, true);
            
            //AllRandomSpawnersActivate.Invoke(StartIndexDown, EndIndexDown);
            yield return new WaitForSeconds(14);
            UtilsBase.ClearBullets<ChainBase>();
            
            Debug.Log("Done");
        }

        public IEnumerator InitPhaseTwo()
        {
            GlobalEvents.OnPhaseChange.AddListener(OnPhaseChange);
            UtilsBase.ClearBullets<Bullet>();
            Debug.Log("Init " + Phases.PhaseTwo);
            
            yield return new WaitForSeconds(2);
            Debug.Log("Done");
        }

        public IEnumerator InitPhaseThree()
        {
            GlobalEvents.OnPhaseChange.AddListener(OnPhaseChange);
            Debug.Log("Init " + Phases.PhaseThree);
            
            yield return new WaitForSeconds(2);
            Debug.Log("Done");
        }

        public IEnumerator InitPhaseFour()
        {
            GlobalEvents.OnPhaseChange.AddListener(OnPhaseChange);
            Debug.Log("Init " + Phases.PhaseFour);
            
            yield return new WaitForSeconds(2);
            Debug.Log("Done");
        }

        private void OnPhaseChange(int phase)
        {
            StopAllCoroutines();
        }
    }
}