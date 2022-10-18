using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Utils;

namespace Character
{
    [CreateAssetMenu(fileName = "new PlayerConfig", menuName = "PlayerConfig")]
    public class PlayerSO: ScriptableObject
    {
        public int health;
        public int maxHealth;
        public float maxValue;
        public float maxLevel;
        public int level;
        public int special;
        public float specialCooldown;
        public float speed;
        public int experience = default;
        public int points = default;
        public GameObject bullet;
        public GameObject targetBullet;
        public GameObject destroyEffect;
        public float targetBulletFrequency;
        public KeyMap levelUpMap = new KeyMap();
        public List<SpecialSettings> specialSettings;
    }
}