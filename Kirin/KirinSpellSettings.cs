using System;
using UnityEngine;

namespace Kirin
{
    [Serializable]
    public class KirinSpellSettings
    {
        public float waitTime;
        public bool change;
        public GameObject bullet;
        public float count;
    }
    
    [Serializable]
    public class KirinSpellSettingsWithDelay: KirinSpellSettings
    {
        public float delay;
    }
}