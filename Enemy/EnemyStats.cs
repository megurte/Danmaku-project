using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyStats: MonoBehaviour
    {
        public float CurrentHp { get; set; }
        public EnemySO enemySo;
        
        private void Awake()
        {
            CurrentHp = enemySo.maxHp;
        }

        private void Update()
        {
            if (!(CurrentHp <= 0)) return;
            Debug.Log($"Destroyed {gameObject.name}");
            Destroy(gameObject);
        }
    }
}