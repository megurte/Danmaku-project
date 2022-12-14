using System;
using System.Collections.Generic;
using Boss.Camilla;
using UnityEngine.PlayerLoop;

namespace Enemy
{
    public class Barrier: EnemyBase
    {
        private void Start()
        {
            CurrentHp = 250;
        }

        private void FixedUpdate()
        {
            CheckHealth();
        }

        public void SwitchIsMagicBarrierActive(bool state)
        {
            CamillaBase.IsMagicBarrierActive = state;
        }
    }
}