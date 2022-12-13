using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "new Enemy", menuName = "Scriptable Object/Enemy")]
    public class EnemySO : ScriptableObject
    {
        public int maxHp;
        public int counter;
        public float cooldown;
        public GameObject bullet;
        public GameObject destroyEffect;
        public Spells spell;
        public MoveSet moveSet;
        public float speed;
        public float angularSpeed;
        public float radius;
        public Vector3 targetPosition;
        public List<LootSettings> lootSettings;
    }
}