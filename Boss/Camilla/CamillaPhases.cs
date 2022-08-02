using System.Collections;
using Bullets;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Boss.Camilla
{
    public class CamillaPhases : MonoBehaviour
    {
        public static UnityEvent<int, int> RandomSpawnersActivate = new UnityEvent<int, int>();
        public static UnityEvent<int, int, bool> WaveChainsSpawn = new UnityEvent<int, int, bool>();

        private const int StartIndexDown = 1;
        private const int EndIndexDown = 14;
        private const int StartIndexUp = 15;
        private const int EndIndexUp = 25;

        public IEnumerator InitPhaseOne()
        {
            GlobalEvents.OnPhaseChange.AddListener(OnPhaseChange);
            Debug.Log("Init " + Phases.PhaseOne);
            
            yield return new WaitForSeconds(2);
            WaveChainsSpawn.Invoke(StartIndexUp, EndIndexUp, true);

            yield return new WaitForSeconds(14);
            UtilsBase.ClearBullets<ChainBase>();
            
            WaveChainsSpawn.Invoke(StartIndexUp, EndIndexUp, true);
            
            RandomSpawnersActivate.Invoke(StartIndexDown, EndIndexDown);
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