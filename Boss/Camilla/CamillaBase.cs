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
        private CamillaScriptableObject _camillaScriptableObject;
        // private bool _isInvulnerable;
        private float _maxHp;
        
        public static bool IsMagicBarrierActive;
        
        [Inject]
        public void Construct(CamillaScriptableObject settings)
        {
            _camillaScriptableObject = settings;
            _maxHp = _camillaScriptableObject.maxHp;
            bar.fillAmount = 100;
            CurrentHp = _maxHp;
        }

        private void Awake()
        {
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
            DropItems(_camillaScriptableObject.lootSettings);
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