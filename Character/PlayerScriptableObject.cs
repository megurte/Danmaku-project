using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Utils;

namespace Character
{
    [CreateAssetMenu(fileName = "new PlayerConfig", menuName = "Scriptable Object/PlayerConfig")]
    public class PlayerScriptableObject: ScriptableObject
    {
        public int health;
        public int maxHealth;
        public int maxValue;
        public int maxLevel;
        public int level;
        public int special;
        public float specialCooldown;
        public float speed;
        public int experience = default;
        public int points = default;
        public GameObject destroyEffect;
        public float targetBulletFrequency;
        public KeyMap levelUpMap = new KeyMap();
        public List<SpecialSettings> specialSettings;
    }
}