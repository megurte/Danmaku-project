using System;
using DefaultNamespace;
using Enemy;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Boss.Camilla
{
    public class CamillaBase: EnemyBase
    {
        private CamillaSO _camillaSo;
        
        private float _lerpSpeed;
        
        public Image bar;

        private float MaxHp { get; set; }
        
        [Inject]
        public void Construct(CamillaSO settings)
        {
            _camillaSo = settings;
            MaxHp = _camillaSo.maxHp;
            _lerpSpeed = _camillaSo.lerpSpeed;
            bar.fillAmount = 100;
            CurrentHp = MaxHp;
        }
        
        private void Awake()
        {
            OnTakingDamageEvent.AddListener(OnTakingDamage);
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
        
        private void OnTakingDamage(float damage, int enemyID)
        {
            if (enemyID == gameObject.GetInstanceID())
                CurrentHp -= damage;
        }
    }
}