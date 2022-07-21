using System;
using DefaultNamespace;
using Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace Kirin
{
    public class KirinStats : EnemyFactory
    {
        public KirinSO kirinSo;
        
        public float lerpSpeed;
        
        public Image bar;

        private float MaxHp { get; set; }
        
        private bool _ultimatePhase = false;

        private void Awake()
        {
            MaxHp = kirinSo.maxHp;
            lerpSpeed = kirinSo.lerpSpeed;
            bar.fillAmount = 100;
            CurrentHp = MaxHp;
            
            OnTakingDamageEvent.AddListener(OnTakingDamage);
            GlobalEventManager.OnPhaseChange.AddListener((int i) => { CurrentHp = MaxHp; });
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
                bar.fillAmount = Mathf.Lerp(bar.fillAmount, CurrentHp / MaxHp, Time.deltaTime * lerpSpeed);
        }
        
        private void OnTakingDamage(float damage, int enemyID)
        {
            if (enemyID == gameObject.GetInstanceID())
                CurrentHp -= damage;
        }
    }
}