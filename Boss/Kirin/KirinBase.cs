using System;
using Enemy;
using Kirin;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Boss.Kirin
{
    public class KirinBase : EnemyBase
    {
        private KirinSO _kirinSo;
        
        private float _lerpSpeed;
        
        public Image bar;

        private float MaxHp { get; set; }
        
        [Inject]
        public void Construct(KirinSO settings)
        {
            _kirinSo = settings;
            MaxHp = _kirinSo.maxHp;
            _lerpSpeed = _kirinSo.lerpSpeed;
            bar.fillAmount = 100;
            CurrentHp = MaxHp;
        }
        
        private void Awake()
        {
            GlobalEvents.OnPhaseChange.AddListener((int i) => { CurrentHp = MaxHp; });
        }

        private void Update()
        {
            HandleBar();
            
            if (CurrentHp <= 0)
            {
                CurrentHp = MaxHp;
                
                GlobalEvents.ChangePhase();
            }
        }

        private  void HandleBar()
        {
            if (Math.Abs(CurrentHp / MaxHp - bar.fillAmount) >= 0)
                bar.fillAmount = Mathf.Lerp(bar.fillAmount, CurrentHp / MaxHp, Time.deltaTime * _lerpSpeed);
        }
    }
}