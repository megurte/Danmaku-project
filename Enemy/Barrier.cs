using System;
using System.Collections.Generic;
using Boss.Camilla;
using UnityEngine.PlayerLoop;
using Zenject;

namespace Enemy
{
    public class Barrier: EnemyBase
    {
        [Inject] private CamillaBase _camillaBase;
        
        private void Start()
        {
            CurrentHp = _camillaBase.GetBarrierMaxHealth();
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