using System;
using Bullets;
using DefaultNamespace;
using UnityEngine;

namespace Kirin
{
    public class KirinScript : MonoBehaviour
    {
        public KirinSO kirinSo;
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
                case (int) Phases.PhaseOne:
                    _kirinPhases.InitPhaseOne(_kirinSpells, _kirinPositions, kirinSo.phaseSpellSettings, kirinSo.phaseMovementPositions);
                    _isPhaseActive = true;
                    break;
                case (int) Phases.PhaseTwo:
                    _kirinPhases.InitPhaseTwo(_kirinSpells, _kirinPositions);
                    _isPhaseActive = true;
                    break;
                case (int) Phases.PhaseThree:
                    _kirinPhases.InitPhaseThree(_kirinSpells, _kirinPositions);
                    _isPhaseActive = true;
                    break;
                case (int) Phases.PhaseFour:
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
        }
    }
}