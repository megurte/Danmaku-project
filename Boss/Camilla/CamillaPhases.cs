using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

namespace Boss.Camilla
{
    public class CamillaPhases : MonoBehaviour
    {
        public static UnityEvent<int, int> OnChainSpawn = new UnityEvent<int, int>();

        private const int StartIndexDown = 1;
        private const int EndIndexDown = 14;
        private const int StartIndexUp = 15;
        private const int EndIndexUp = 25;

        public IEnumerator InitPhaseOne()
        {
            GlobalEvents.OnPhaseChange.AddListener(OnPhaseChange);
            Debug.Log("Init " + Phases.PhaseOne);
            
            yield return new WaitForSeconds(2);
            OnChainSpawn.Invoke(StartIndexUp, EndIndexUp);

            yield return new WaitForSeconds(10);
            OnChainSpawn.Invoke(StartIndexDown, EndIndexDown);
            
            Debug.Log("Done");
        }

        public IEnumerator InitPhaseTwo()
        {
            GlobalEvents.OnPhaseChange.AddListener(OnPhaseChange);
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