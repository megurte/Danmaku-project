using System;
using Drop;
using UnityEngine;

namespace Enemy
{
    [Serializable]
    public struct LootSettings
    {
        public DropBase dropItem;
        public int dropNumber;
        public float chance;
    }
}