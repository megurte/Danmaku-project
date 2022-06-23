using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public class KeyMap: Dictionary<int, int>
    {
        [SerializeField] public List<int> keys = new List<int>();
        [SerializeField] public List<int> values = new List<int>();
    }

    public static class KeyMapExtend
    {
        [ContextMenu(nameof(Add))]
        public static void Add(this KeyMap @this, int key, int value)
        {
            @this.Add(key, value);
        }

        [ContextMenu(nameof(Remove))]
        public static void Remove(this KeyMap @this, int key, int value)
        {
            @this.Remove(key);
        }
    }
}