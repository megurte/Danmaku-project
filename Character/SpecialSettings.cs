using System;
using UnityEngine;

namespace Character
{
    [Serializable]
    public class SpecialSettings
    {
        public GameObject specialGameObject;
        public Vector3 specialPosition;
        public int damageToBoss;
        public float specialCooldown;
        public int special;
        public int maxSpecials;
    }
}