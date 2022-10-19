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
        private bool _isBarrierActive;
        private float _maxHp;

        [Inject]
        public void Construct(CamillaSO settings)
        {
            _camillaSo = settings;
            _maxHp = _camillaSo.maxHp;
            bar.fillAmount = 100;
            CurrentHp = _maxHp;
        }

        private void Awake()
        {
            CamillaPhases.CreateMagicalBarrier.AddListener(()=> _isBarrierActive = true);
            GlobalEvents.OnBossFightFinish.AddListener(OnBossFightFinished);
            GlobalEvents.OnPhaseChange.AddListener((int i) => { CurrentHp = _maxHp; });
        }

        private void Update()
        {
            HandleBar();

            if (CurrentHp <= 0)
            {
                CurrentHp = _maxHp;

                GlobalEvents.ChangePhase();
            }
        }

        private void HandleBar()
        {
            if (Math.Abs(CurrentHp / _maxHp - bar.fillAmount) >= 0)
                bar.fillAmount = CurrentHp / _maxHp;
        }

        public void OnBossFightFinished()
        {
            // Dialog init
            DropItems(_camillaSo.lootSettings);
            BossUI(false);
            Destroy(gameObject);
        }
        
        public void BossUI(bool isActive)
        {
            bossBarUI.SetActive(isActive);
            bossTimerUI.SetActive(isActive);
        }
    }
}