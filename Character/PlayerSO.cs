using UnityEngine;

namespace Character
{
    [CreateAssetMenu(fileName = "new PlayerConfig", menuName = "PlayerConfig")]
    public class PlayerSO: ScriptableObject
    {
        public float health;
        public float maxValue;
        public float maxLevel;
        public float special;
        public float speed;
        public int level;
        public int exp = default;
        public int points = default;
        public GameObject bullet;
        public GameObject targetBullet;
        public float targetBulletFrequency;
    }
}