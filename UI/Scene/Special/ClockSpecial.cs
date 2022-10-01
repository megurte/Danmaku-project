using UnityEngine;

namespace UI.Scene.Special
{
    public class ClockSpecial : MonoBehaviour
    {
        public GameObject hourArrow;
    
        public GameObject minuteArrow;
    
        public float timeRemaining = default;

        private const float DelayHourArrow = 1f;
    
        private float _delayTimer = default;

        private void Start()
        {
            _delayTimer = DelayHourArrow;
        }

        private void FixedUpdate()
        {
            TimerRun();

            if (timeRemaining <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void TimerRun()
        {
            if (!(timeRemaining > 0)) return;
            
            timeRemaining -= Time.deltaTime;
            
            minuteArrow.transform.Rotate(new Vector3(0, 0, -360) * Time.deltaTime);
            hourArrow.transform.Rotate(_delayTimer == DelayHourArrow ? new Vector3(0, 0, -30) : new Vector3(0, 0, 0));

            _delayTimer-= Time.deltaTime;

            if (_delayTimer <= 0)
                _delayTimer = DelayHourArrow;
        }
    }
}
