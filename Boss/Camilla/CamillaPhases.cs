using System;
using System.Collections;
using Bullets;
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
        public static readonly UnityEvent<SpellPropellerBulletShootSettings> PropellerBulletSpawn = new UnityEvent<SpellPropellerBulletShootSettings>();
        public static readonly UnityEvent<SpellSettingsWithCount> CircleBulletWithRandomColorsSpawn = new UnityEvent<SpellSettingsWithCount>();
        public static readonly UnityEvent<SpellRandomShootingSettings> RandomShooting = new UnityEvent<SpellRandomShootingSettings>();
        public static readonly UnityEvent<CommonSpellSettingsWithTarget> TargetPositionShooting = new UnityEvent<CommonSpellSettingsWithTarget>();
        public static readonly UnityEvent<SpellReverseBulletShootSettings> SpiralBulletSpawn = new UnityEvent<SpellReverseBulletShootSettings>();
        public static readonly UnityEvent<SpellReverseBulletShootSettings> ReverseBulletSpawn = new UnityEvent<SpellReverseBulletShootSettings>();
        public static readonly UnityEvent CreateMagicalBarrier = new UnityEvent();
        public static readonly UnityEvent StopAllSpells = new UnityEvent();
        
        private CamillaSO _camillaSettings;
        private CamillaPhaseSettings _camillaPhaseSettings;

        #region Spawner Indexes
        private const int StartIndexDown = 1;
        private const int EndIndexDown = 14;
        private const int StartIndexUp = 15;
        private const int EndIndexUp = 25;
        #endregion

        [Inject] 
        public void Construct(CamillaPhaseSettings phaseSettings, CamillaSO enemySettings)
        {
            _camillaPhaseSettings = phaseSettings;
            _camillaSettings = enemySettings;
        }

        public IEnumerator InitPhaseOne()
        {
            UtilsBase.ClearBullets<Bullet>();
            GlobalEvents.OnPhaseChange.AddListener(OnPhaseChange);

            yield return new WaitForSeconds(2);
            
            RandomShooting.Invoke(_camillaPhaseSettings.phaseOneSettings.randomShooting);

            yield return new WaitForSeconds(1);

            while (true)
            {
                ReverseBulletSpawn.Invoke(_camillaPhaseSettings.phaseOneSettings.reverseBulletShootIteration1);
            
                yield return new WaitForSeconds(1);
                ReverseBulletSpawn.Invoke(_camillaPhaseSettings.phaseOneSettings.reverseBulletShootIteration2);
                
                yield return new WaitForSeconds(1);
                SpiralBulletSpawn.Invoke(_camillaPhaseSettings.phaseOneSettings.spiralBulletShootIteration1);
            
                yield return new WaitForSeconds(0.5f);
                SpiralBulletSpawn.Invoke(_camillaPhaseSettings.phaseOneSettings.spiralBulletShootIteration2);
            }
        }

        public IEnumerator InitPhaseTwo()
        {
            GlobalEvents.OnPhaseChange.AddListener(OnPhaseChange);
            
            yield return new WaitForSeconds(2);

            PropellerBulletSpawn.Invoke(_camillaPhaseSettings.phaseTwoSettings.spellPropellerBulletShoot);
            RandomShooting.Invoke(_camillaPhaseSettings.phaseTwoSettings.randomShooting);

            while (true)
            {
                yield return new WaitForSeconds(2);

                RandomSpawnersActivate.Invoke(StartIndexUp, EndIndexUp, 3);

                yield return new WaitForSeconds(6);
                UtilsBase.ClearBullets<ChainBase>();
            }
        }

        public IEnumerator InitPhaseThree()
        {
            GlobalEvents.OnPhaseChange.AddListener(OnPhaseChange);
            
            yield return new WaitForSeconds(2);
            PropellerBulletSpawn.Invoke(_camillaPhaseSettings.phaseThreeSettings.propellerBulletShoot);
            RandomShooting.Invoke(_camillaPhaseSettings.phaseThreeSettings.randomShootingIteration1);
            RandomShooting.Invoke(_camillaPhaseSettings.phaseThreeSettings.randomShootingIteration2);

            while (true)
            {
                yield return new WaitForSeconds(2);

                RandomSpawnersActivate.Invoke(StartIndexUp, EndIndexUp, 2);

                yield return new WaitForSeconds(8);
                
                UtilsBase.ClearBullets<ChainBase>();
            }
        }

        public IEnumerator InitPhaseFour()
        {
            GlobalEvents.OnPhaseChange.AddListener(OnPhaseChange);
            
            CreateMagicalBarrier.Invoke();
            
            yield return new WaitForSeconds(2);

            PropellerBulletSpawn.Invoke(_camillaPhaseSettings.phaseFourSettings.propellerBulletShoot);
            AllRandomSpawnersActivate.Invoke(StartIndexDown, EndIndexDown);
            
            yield return new WaitForSeconds(14);
            UtilsBase.ClearBullets<ChainBase>();
        }

        private void OnPhaseChange(int phase)
        {
            StopAllCoroutines();
            StopAllSpells.Invoke();
            UtilsBase.ClearBullets<Bullet>();

            if (_camillaSettings.maxPhases <= phase)
            {
                Debug.Log("Init " + phase + " Phase");
            }
        }
    }
}