using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Boss.Camilla
{
    [CreateAssetMenu(fileName = "new Camilla", menuName = "Scriptable Object/Camilla")]
    public class CamillaScriptableObject: ScriptableObject
    {
        public float maxHp;
        public int maxPhases;
        
        public List<LootSettings> lootSettings = new List<LootSettings>();
        public List<GameObject> bullets = new List<GameObject>();
    }
}