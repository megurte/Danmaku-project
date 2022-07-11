using System;
using UnityEngine;

namespace Kirin
{
    [Serializable]
    public class KirinSpellSettings
    {
        public SpellName spellName;
        public float waitTime;
        public bool change;
        public GameObject bullet;
        public int count;
    }
    
    [Serializable]
    public class KirinSpellSettingsWithDelay: KirinSpellSettings
    {
        public float delay;
    }
    
    [Serializable]
    public class KirinMoveSettings
    {
        public float waitTime;
        public Vector2 position;
    }

    [Serializable]
    public enum SpellName
    {
        SpiralWithDelay,
        Circle,
    }
}