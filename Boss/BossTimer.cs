using System;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

namespace Boss
{
    public class BossTimer : MonoBehaviour
    {
        public TextMeshProUGUI textUI;
        
        public float timeRemaining = default;
        
        public bool timerIsRunning = default;
        
        private int _phaseIndex = default;
        
        [Serializable]
        public struct PhaseTimer
        {
            public float seconds;
            public float milSec;
        }

        public List<PhaseTimer> timers;
        
        private void Start()
        {
            GlobalEvents.OnPhaseChange.AddListener(OnPhaseChange);
        }
        
        private void FixedUpdate()
        {
            var timer = timers[0];
            
            if (textUI.text == "00:00")
            {
                textUI.text = $"{timer.seconds}:{timer.milSec}0";
                TimerInit(timer);
            }
                
            TimerRun();
            UpdateTimerText(timer.seconds, timer.milSec);
        }

        private void TimerInit(PhaseTimer timer)
        {
            timeRemaining = timer.seconds;
            timerIsRunning = true;
        }
        
        private void TimerRun()
        {
            if (!timerIsRunning) return;
            
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                
                GlobalEvents.ChangePhase();
            }
        }

        private void UpdateTimerText(float seconds, float milliseconds = 10)
        {
            if (seconds < 0 || milliseconds < 0)
            {
                throw new Exception("BossTimer.UpdateTimerText(): seconds or milliseconds lower then 0");
            }
            
            seconds = Mathf.FloorToInt(timeRemaining % 100);  
            milliseconds = (timeRemaining % 1) * 100;
            textUI.text = $"{seconds:00}:{milliseconds:00}";
        }

        private void OnPhaseChange(int phase)
        {
            if (phase <= timers.Count)
            {
                _phaseIndex = phase - 1;
                
                TimerInit(timers[_phaseIndex]);
            }
            else
                timerIsRunning = false;
        }
    }
}