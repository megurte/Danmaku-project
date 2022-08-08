using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Boss
{
    public class BossTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textUI;

        [SerializeField] private int pointsForEachSecondLeft;

        private float _timeRemaining = default;
        
        private bool _timerIsRunning = default;
        
        private int _phaseIndex = default;

        private int _scoreSeconds;
        
        [Serializable]
        public struct PhaseTimer
        {
            public float seconds;
            public float milSec;
        }

        [SerializeField] private List<PhaseTimer> timers;
        
        private Dictionary<int, int> _phasesScore = new Dictionary<int, int>();

        public static readonly UnityEvent<Dictionary<int, int>> TranslateTimerData = new UnityEvent<Dictionary<int, int>>(); 

        private void Start()
        {
            GlobalEvents.OnPhaseChange.AddListener(OnPhaseChange);
            GlobalEvents.OnBossFightFinish.AddListener(OnBossFightFinished);
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
            _timeRemaining = timer.seconds;
            _timerIsRunning = true;
        }
        
        private void TimerRun()
        {
            if (!_timerIsRunning) return;
            
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
            }
            else
            {
                _timeRemaining = 0;
                _timerIsRunning = false;
                
                GlobalEvents.ChangePhase();
            }
        }

        private void UpdateTimerText(float seconds, float milliseconds = 10)
        {
            if (seconds < 0 || milliseconds < 0)
            {
                throw new Exception("BossTimer.UpdateTimerText(): seconds or milliseconds lower then 0");
            }
            
            seconds = Mathf.FloorToInt(_timeRemaining % 100);
            milliseconds = (_timeRemaining % 1) * 100;
            _scoreSeconds = (int)seconds;
            textUI.text = $"{seconds:00}:{milliseconds:00}";
        }

        private int CalculatePointsForTimeRemaining()
        {
            return _scoreSeconds * pointsForEachSecondLeft;
        }

        private void OnPhaseChange(int phase)
        {
            if (phase <= timers.Count)
            {
                _phasesScore.Add(_scoreSeconds, CalculatePointsForTimeRemaining());
                
                _phaseIndex = phase - 1;
                
                TimerInit(timers[_phaseIndex]);
            }
            else
                _timerIsRunning = false;
        }

        private void OnBossFightFinished()
        {
            _phasesScore.Add(_scoreSeconds, CalculatePointsForTimeRemaining());
            TranslateTimerData.Invoke(_phasesScore);
        }
    }
}