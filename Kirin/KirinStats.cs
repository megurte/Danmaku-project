using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Kirin
{
    public class KirinStats : MonoBehaviour
    {
        public float CurrentHp { get; set; }
        private float MaxHp { get; set; }
        
        public float LerpSpeed = 2f;
        
        private bool _ultimatePhase = false;
        
        public Image bar;
        
        private void Start()
        {
            GlobalEventManager.OnPhaseChange.AddListener((int i) => { CurrentHp = MaxHp; });
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
                GlobalEventManager.ChangePhase();
            }

            if (CurrentHp / MaxHp < 0.3f)
            {
                _ultimatePhase = true;
            }
        }

        private  void HandleBar()
        {
            if (Math.Abs(CurrentHp / MaxHp - bar.fillAmount) >= 0)
                bar.fillAmount = Mathf.Lerp(bar.fillAmount, CurrentHp / MaxHp, Time.deltaTime * LerpSpeed);
        }
    }
}