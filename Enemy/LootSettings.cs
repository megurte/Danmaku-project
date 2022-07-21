using System;
using UnityEngine;

namespace Enemy
{
    [Serializable]
    public struct LootSettings
    {
        public GameObject dropItem;
        public int dropNumber;
        public float chance;
    }
}