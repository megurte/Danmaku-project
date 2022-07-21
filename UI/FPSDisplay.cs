using TMPro;
using UnityEngine;

namespace UI
{
    public class FPSDisplay : MonoBehaviour
    {
        public TextMeshProUGUI fpsText;

        private const float PollingTime = 1f;
        private float _time = default;
        private int _frameCount = default;

        private void Update()
        {
            _time += Time.deltaTime;
            _frameCount++;

            if (_time >= PollingTime)
            {
                fpsText.text = CalculateFrameRate() + " FPS";
                _time -= PollingTime;
                _frameCount = 0;
            }
        }

        private int CalculateFrameRate()
        {
            return Mathf.RoundToInt(_frameCount / _time);
        }
    }
}
