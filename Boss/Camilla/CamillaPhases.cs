using System.Collections;
using DefaultNamespace;
using UnityEngine;

namespace Boss.Camilla
{
    public class CamillaPhases : MonoBehaviour
    {
        public IEnumerator InitPhaseOne()
        {
            GlobalEvents.OnPhaseChange.AddListener(OnPhaseChange);
            Debug.Log("Init " + Phases.PhaseOne);

            yield return new WaitForSeconds(2);
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