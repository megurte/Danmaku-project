using System;
using Spells;
using UnityEngine;

namespace Boss.Camilla
{
    public class CamillaPhaseSettings: ScriptableObject
    {
        public PhaseOneSettings phaseOneSettings;
        public PhaseTwoSettings phaseTwoSettings;
        public PhaseThreeSettings phaseThreeSettings;
        public PhaseFourSettings phaseFourSettings;
        
        [Serializable]
        public struct PhaseOneSettings
        {
            public SpellRandomShootingSettings randomShooting;
            public SpellReverseBulletShootSettings reverseBulletShootIteration1;
            public SpellReverseBulletShootSettings reverseBulletShootIteration2;
            public SpellReverseBulletShootSettings spiralBulletShootIteration1;
            public SpellReverseBulletShootSettings spiralBulletShootIteration2;
        }
        [Serializable]
        public struct PhaseTwoSettings
        {
            public SpellPropellerBulletShootSettings spellPropellerBulletShoot;
            public SpellRandomShootingSettings randomShooting;
        }
        [Serializable]
        public struct PhaseThreeSettings
        {
            public SpellPropellerBulletShootSettings propellerBulletShoot;
            public SpellRandomShootingSettings randomShootingIteration1;
            public SpellRandomShootingSettings randomShootingIteration2;
        }
        [Serializable]
        public struct PhaseFourSettings
        {
            public SpellPropellerBulletShootSettings propellerBulletShoot;
        }
    }
}