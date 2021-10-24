using System;
using UnityEngine;
using UnityEngine.UI;

namespace Kirin
{
    public class KirinStats : MonoBehaviour
    {
        public float CurrentHp { get; set; }
        public float MaxHp { get; set; }
        public float lerpspeed = 2f;
        private bool _ultimatePhase = false;
        private int _phase = 1;
        public Image bar;
        
        private void Start()
        {
            MaxHp = 1000;
            bar.fillAmount = 100;
            CurrentHp = MaxHp;
        }

        private void Update()
        {
            HandleBar();
            if (CurrentHp <= 0)
            {
                _ultimatePhase = false;
                CurrentHp = MaxHp;
                _phase += 1;
            }

            if (CurrentHp / MaxHp < 0.3f)
            {
                _ultimatePhase = true;
            }
        }

        private  void HandleBar()
        {
            if (CurrentHp / MaxHp != bar.fillAmount)
                bar.fillAmount = Mathf.Lerp(bar.fillAmount, CurrentHp / MaxHp, Time.deltaTime * lerpspeed);
        }
    }
}