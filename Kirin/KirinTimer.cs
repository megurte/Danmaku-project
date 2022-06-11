using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kirin
{
    public class KirinTimer : MonoBehaviour
    {
        public GameObject textObj;
        
        private Text _text;
        
        public float timeRemaining = 0;
        
        public bool timerIsRunning = false;

        
        [Serializable]
        public struct PhaseTimer
        {
            public float seconds;
            public float milSec;
        }

        public List<PhaseTimer> timers;

        private void Start()
        {
            _text = textObj.GetComponent<Text>();
        }
        
        private void FixedUpdate()
        {
            var timer = timers[0];
            
            if (_text.text == "00:00")
            {
                _text.text = $"{timer.seconds}:{timer.milSec}0";
                TimerInit(timer);
            }
                
            TimerRun();
            UpdateTimerText(timer.seconds, timer.milSec);
        }

        public void TimerInit(PhaseTimer timer)
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
            }
        }

        private void UpdateTimerText(float seconds, float milliseconds = 10)
        {
            if (seconds < 0 || milliseconds < 0)
            {
                throw new Exception("KirinTimer.UpdateTimerText(): seconds or milliseconds lower then 0");
            }
            
            seconds = Mathf.FloorToInt(timeRemaining % 100);  
            milliseconds = (timeRemaining % 1) * 1000;
            _text.text = $"{seconds:00}:{milliseconds:00}";
        }
    }
}