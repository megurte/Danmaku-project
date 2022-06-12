using System;
using DefaultNamespace;
using UnityEngine;

namespace Kirin
{
    public class KirinScript : MonoBehaviour
    {
        private KirinSpells _kirinSpells;
        private int _phaseNumber = 1;
        private bool _isPhaseActive = false;

        private void Awake()
        {
            _kirinSpells = GetComponent<KirinSpells>();
            GlobalEventManager.OnPhaseChange.AddListener(OnPhaseChange);
        }

        private void Update()
        {
            if (_isPhaseActive) return;
            switch (_phaseNumber)
            {
                case 1:
                    KirinPhases.InitPhaseOne(_kirinSpells);
                    _isPhaseActive = true;
                    break;
                case 2:
                    KirinPhases.InitPhaseTwo(_kirinSpells);
                    _isPhaseActive = true;
                    break;
                case 3:
                    KirinPhases.InitPhaseThree(_kirinSpells);
                    _isPhaseActive = true;
                    break;
                case 4:
                    KirinPhases.InitPhaseFour(_kirinSpells);
                    _isPhaseActive = true;
                    break;
            }
        }

        private void OnPhaseChange(int phase)
        {
            _phaseNumber = phase;
            _isPhaseActive = false;
        }
    }
}