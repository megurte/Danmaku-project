using UnityEngine;
using Zenject;

namespace Boss.Camilla
{
    public class CamillaPhaseChanger: MonoBehaviour
    {
        [Inject] private CamillaScriptableObject _camillaSettings;
        private CamillaPhases _camillaPhases;
        private int _phaseNumber = 1;
        private bool _isPhaseActive = default;

        private void Awake()
        {
            _camillaPhases = GetComponent<CamillaPhases>();
            
            GlobalEvents.OnPhaseChange.AddListener(OnPhaseChange);
        }

        private void Update()
        {
            if (_camillaSettings.maxPhases < _phaseNumber)
            {
                GlobalEvents.OnBossFightFinished();
            }
            
            if (_isPhaseActive) return;
            
            switch (_phaseNumber)
            {
                case (int) Phases.PhaseOne:
                    StartCoroutine(_camillaPhases.InitPhaseOne());
                    _isPhaseActive = true;
                    break;
                case (int) Phases.PhaseTwo:
                    StartCoroutine(_camillaPhases.InitPhaseTwo());
                    _isPhaseActive = true;
                    break;
                case (int) Phases.PhaseThree:
                    StartCoroutine(_camillaPhases.InitPhaseThree());
                    _isPhaseActive = true;
                    break;
                case (int) Phases.PhaseFour:
                    StartCoroutine(_camillaPhases.InitPhaseFour());
                    _isPhaseActive = true;
                    break;
            }
        }

        private void OnPhaseChange(int phase)
        {
            StopAllCoroutines();
            _phaseNumber = phase;
            _isPhaseActive = false;
        }
    }
}