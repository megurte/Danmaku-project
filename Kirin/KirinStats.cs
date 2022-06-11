using System;
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
        
        private int _phase = 1;
        
        public Image bar;
        
        private KirinTimer _kirinTimer;
        
        private void Start()
        {
            MaxHp = 1000;
            bar.fillAmount = 100;
            CurrentHp = MaxHp;
            
            _kirinTimer = GameObject.FindWithTag("TimerManager").GetComponent<KirinTimer>();
        }

        private void Update()
        {
            HandleBar();
            if (CurrentHp <= 0)
            {
                _ultimatePhase = false;
                CurrentHp = MaxHp;
                _phase += 1;

                if (_phase <= _kirinTimer.timers.Count)
                {
                    _kirinTimer.TimerInit(_kirinTimer.timers[_phase - 1]);
                }
                else
                {
                    _kirinTimer.timerIsRunning = false;
                }
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