using System;
using System.Collections.Generic;
using UnityEngine.PlayerLoop;

namespace Enemy
{
    public class Barrier: EnemyBase
    {
        private void Start()
        {
            CurrentHp = 100;
        }

        private void Update()
        {
            CheckHealth();
        }
    }
}