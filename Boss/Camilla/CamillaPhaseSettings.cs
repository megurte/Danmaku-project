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
        public SpawnerIndexes spawnerIndexes;
        
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
            public SpellPropellerBulletShootSettings propellerBulletShootIteration1;
            public SpellPropellerBulletShootSettings propellerBulletShootIteration2;
            public SpellPropellerBulletShootSettings propellerBulletShootIteration3;
            public SpellPropellerBulletShootSettings propellerBulletShootIteration4;
            
            public SpellRandomShootingSettings randomShooting;
        }

        [Serializable]
        public struct SpawnerIndexes
        {
            public int startIndexDown;
            public int endIndexDown;
            public int startIndexUp;
            public int endIndexUp;
        }
    }
}