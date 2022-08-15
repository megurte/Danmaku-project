using System;
using Enemy;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Boss.Camilla
{
    public class CamillaBase : EnemyBase, IBoss
    {
        [SerializeField] private Image bar;
        
        [SerializeField] private GameObject bossTimerUI;
        
        [SerializeField] private GameObject bossBarUI;

        private CamillaSO _camillaSo;

        private float MaxHp { get; set; }

        [Inject]
        public void Construct(CamillaSO settings)
        {
            _camillaSo = settings;
            MaxHp = _camillaSo.maxHp;
            bar.fillAmount = 100;
            CurrentHp = MaxHp;
        }

        private void Awake()
        {
            OnTakingDamageEvent.AddListener(OnTakingDamage);
            GlobalEvents.OnBossFightFinish.AddListener(OnBossFightFinished);
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

        private void HandleBar()
        {
            if (Math.Abs(CurrentHp / MaxHp - bar.fillAmount) >= 0)
                bar.fillAmount = CurrentHp / MaxHp;
        }

        public void OnBossFightFinished()
        {
            // Dialog init
            DropItems(_camillaSo.lootSettings);
            BossUI(false);
            Destroy(gameObject);
        }
        
        private void BossUI(bool isActive)
        {
            bossBarUI.SetActive(isActive);
            bossTimerUI.SetActive(isActive);
        }
    }
}