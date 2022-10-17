using System;
using UnityEngine;

namespace Boss.Kirin
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
}