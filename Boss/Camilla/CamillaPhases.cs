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
        public static readonly UnityEvent<SpellSettingsWithDirectionAndAngle> SpiralBulletSpawn = new UnityEvent<SpellSettingsWithDirectionAndAngle>();
        public static readonly UnityEvent<SpellSettingsWithDirectionAndAngle> ReverseBulletSpawn = new UnityEvent<SpellSettingsWithDirectionAndAngle>();
        public static readonly UnityEvent<PropellerSpellSettings> PropellerBulletSpawn = new UnityEvent<PropellerSpellSettings>();
        public static readonly UnityEvent<SpellSettingsWithCount> CircleBulletWithRandomColorsSpawn = new UnityEvent<SpellSettingsWithCount>();
        public static readonly UnityEvent<CommonSpellSettingsWithDelay> RandomShooting = new UnityEvent<CommonSpellSettingsWithDelay>();
        public static readonly UnityEvent<CommonSpellSettingsWithTarget, float> TargetPositionShooting = new UnityEvent<CommonSpellSettingsWithTarget, float>();
        public static readonly UnityEvent StopAllSpells = new UnityEvent();
        
        private CamillaSO _camillaSettings;
        private Vector3 _self;

        #region Bullets
        private GameObject _fireBulletBlue;
        private GameObject _fireBulletOrange;
        private GameObject _fireSmallBulletBlue;
        private GameObject _fireSmallBulletOrange;
        private GameObject _timedFireBullet;
        private GameObject _timedFireSmallBullet;
        private GameObject _timedFireSmallBulletBlue;

        #endregion
        
        #region Spawner Indexes
        private const int StartIndexDown = 1;
        private const int EndIndexDown = 14;
        private const int StartIndexUp = 15;
        private const int EndIndexUp = 25;
        #endregion

        [Inject] 
        public void Construct(CamillaSO settings)
        {
            _camillaSettings = settings;
            _fireBulletBlue = _camillaSettings.bullets[0];
            _fireBulletOrange = _camillaSettings.bullets[1];
            _fireSmallBulletBlue = _camillaSettings.bullets[2];
            _fireSmallBulletOrange = _camillaSettings.bullets[3];
            _timedFireBullet = _camillaSettings.bullets[4];
            _timedFireSmallBullet = _camillaSettings.bullets[5];
            _timedFireSmallBulletBlue = _camillaSettings.bullets[6];
        }

        public IEnumerator InitPhaseOne()
        {
            UtilsBase.ClearBullets<Bullet>();
            GlobalEvents.OnPhaseChange.AddListener(OnPhaseChange);

            yield return new WaitForSeconds(2);

            _self = transform.position;

            RandomShooting.Invoke(new CommonSpellSettingsWithDelay(_timedFireSmallBulletBlue, _self, 1, 0.01f));

            yield return new WaitForSeconds(1);

            while (true)
            {
                ReverseBulletSpawn.Invoke(new SpellSettingsWithDirectionAndAngle(_fireBulletOrange, _self, 2, 
                    70, false, 90, 0.09f));
            
                yield return new WaitForSeconds(1);
                ReverseBulletSpawn.Invoke(new SpellSettingsWithDirectionAndAngle(_fireBulletOrange, _self, 2, 
                    70, true, 90, 0.09f));
            
            
                yield return new WaitForSeconds(1);
                SpiralBulletSpawn.Invoke(new SpellSettingsWithDirectionAndAngle(_fireBulletOrange, _self, 2, 70, 
                    false, 90, 0.09f));
            
                yield return new WaitForSeconds(0.5f);
                SpiralBulletSpawn.Invoke(new SpellSettingsWithDirectionAndAngle(_fireBulletOrange, _self, 2, 
                    70, false, 90, 0.09f));
            }
        }

        public IEnumerator InitPhaseTwo()
        {
            GlobalEvents.OnPhaseChange.AddListener(OnPhaseChange);
            
            yield return new WaitForSeconds(2);

            PropellerBulletSpawn.Invoke(new PropellerSpellSettings(_fireBulletOrange,
                _self, 2, 30, 10, 0.001f, 30));
            RandomShooting.Invoke(new CommonSpellSettingsWithDelay(_fireBulletOrange,
                _self, 1, 0.001f));

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

            
            yield return null;
        }

        public IEnumerator InitPhaseFour()
        {
            GlobalEvents.OnPhaseChange.AddListener(OnPhaseChange);
            
            yield return new WaitForSeconds(2);

            PropellerBulletSpawn.Invoke(new PropellerSpellSettings(_fireBulletOrange, _self, 2, 30, 
                10, 0.1f, 30));
            RandomShooting.Invoke(new CommonSpellSettingsWithDelay(_fireBulletOrange, _self, 1, 0.01f));
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