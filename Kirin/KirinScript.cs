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
        private KirinPhases _kirinPhases;
        private int _phaseNumber = 1;
        private bool _isPhaseActive = false;

        private void Awake()
        {
            _kirinSpells = GetComponent<KirinSpellsAPI>();
            _kirinPhases = GetComponent<KirinPhases>();
            _kirinPositions = GetComponent<KirinMove>();
            GlobalEventManager.OnPhaseChange.AddListener(OnPhaseChange);
        }

        private void Update()
        {
            if (_isPhaseActive) return;
            switch (_phaseNumber)
            {
                case 1:
                    _kirinPhases.InitPhaseOne(_kirinSpells, _kirinPositions);
                    _isPhaseActive = true;
                    break;
                case 2:
                    _kirinPhases.InitPhaseTwo(_kirinSpells, _kirinPositions);
                    _isPhaseActive = true;
                    break;
                case 3:
                    _kirinPhases.InitPhaseThree(_kirinSpells, _kirinPositions);
                    _isPhaseActive = true;
                    break;
                case 4:
                    _kirinPhases.InitPhaseFour(_kirinSpells, _kirinPositions);
                    _isPhaseActive = true;
                    break;
            }
        }

        private void OnPhaseChange(int phase)
        {
            StopAllCoroutines();
            _phaseNumber = phase;
            _isPhaseActive = false;
            var test = 3;
        }
    }
}