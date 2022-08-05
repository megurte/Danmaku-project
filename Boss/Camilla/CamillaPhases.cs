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
        public static readonly UnityEvent<SpellSettingsWithDirectionAndAngle> ForTest = new UnityEvent<SpellSettingsWithDirectionAndAngle>();

        public static readonly UnityEvent<int, int> AllRandomSpawnersActivate = new UnityEvent<int, int>();
        public static readonly UnityEvent<int, int, int> RandomSpawnersActivate = new UnityEvent<int, int, int>();
        public static readonly UnityEvent<int, int, bool> WaveChainsSpawn = new UnityEvent<int, int, bool>();
        public static readonly UnityEvent<SpellSettingsWithDirectionAndAngle> SpiralBulletSpawn = new UnityEvent<SpellSettingsWithDirectionAndAngle>();
        public static readonly UnityEvent<SpellSettingsWithDirectionAndAngle> ReverseBulletSpawn = new UnityEvent<SpellSettingsWithDirectionAndAngle>();
        public static readonly UnityEvent<PropellerSpellSettings> PropellerBulletSpawn = new UnityEvent<PropellerSpellSettings>();
        public static readonly UnityEvent<SpellSettingsWithCount> CircleBulletWithRandomColorsSpawn = new UnityEvent<SpellSettingsWithCount>();
        public static readonly UnityEvent<CommonSpellSettingsWithDelay> RandomShooting = new UnityEvent<CommonSpellSettingsWithDelay>();
        public static readonly UnityEvent StopAllSpells = new UnityEvent();
        
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
            UtilsBase.ClearBullets<Bullet>();
            GlobalEvents.OnPhaseChange.AddListener(OnPhaseChange);
            Debug.Log("Init " + Phases.PhaseOne);

            yield return new WaitForSeconds(2);
            PropellerBulletSpawn.Invoke(new PropellerSpellSettings(_camillaSettings.bullets[1],
                transform.position, 2, 30, 10, 0.001f, 30));
            RandomShooting.Invoke(new CommonSpellSettingsWithDelay(_camillaSettings.bullets[1],
                transform.position, 1, 0.01f));
            
            yield return new WaitForSeconds(3);
            //AllRandomSpawnersActivate.Invoke(StartIndexDown, EndIndexDown);

            /*yield return new WaitForSeconds(2);
            ReverseBulletSpawn.Invoke(new SpellSettingsWithDirectionAndAngle(_camillaSettings.bullets[1],
                transform.position, 2, 70, false, 90, 0.09f));
            
            yield return new WaitForSeconds(1);
            ReverseBulletSpawn.Invoke(new SpellSettingsWithDirectionAndAngle(_camillaSettings.bullets[1],
                transform.position, 2, 70, true, 90, 0.09f));
            
            
            yield return new WaitForSeconds(1);
            SpiralBulletSpawn.Invoke(new SpellSettingsWithDirectionAndAngle(_camillaSettings.bullets[1],
                transform.position, 2, 70, false, 90, 0.09f));
            
            yield return new WaitForSeconds(0.5f);
            SpiralBulletSpawn.Invoke(new SpellSettingsWithDirectionAndAngle(_camillaSettings.bullets[1],
                transform.position, 2, 70, true, 90, 0.09f));*/
            
            
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
            StopAllSpells.Invoke();
        }
    }
}