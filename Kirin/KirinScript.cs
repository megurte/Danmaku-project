using System;
using Bullets;
using DefaultNamespace;
using UnityEngine;

namespace Kirin
{
    public class KirinScript : MonoBehaviour
    {
        private KirinSpellsAPI _kirinSpells;
        private KirinMove _kirinPositions;
        private int _phaseNumber = 1;
        private bool _isPhaseActive = false;

        private void Awake()
        {
            _kirinSpells = GetComponent<KirinSpellsAPI>();
            _kirinPositions = GetComponent<KirinMove>();
            GlobalEventManager.OnPhaseChange.AddListener(OnPhaseChange);
        }

        private void Update()
        {
            if (_isPhaseActive) return;
            switch (_phaseNumber)
            {
                case 1:
                    KirinPhases.InitPhaseOne(_kirinSpells, _kirinPositions);
                    _isPhaseActive = true;
                    break;
                case 2:
                    KirinPhases.InitPhaseTwo(_kirinSpells, _kirinPositions);
                    _isPhaseActive = true;
                    break;
                case 3:
                    KirinPhases.InitPhaseThree(_kirinSpells, _kirinPositions);
                    _isPhaseActive = true;
                    break;
                case 4:
                    KirinPhases.InitPhaseFour(_kirinSpells, _kirinPositions);
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