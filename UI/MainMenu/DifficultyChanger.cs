using System;
using Character;
using Stage;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.MainMenu
{
    public class DifficultyChanger : MonoBehaviour
    {
        [SerializeField] private Difficulty difficultyValue = Difficulty.Default;

        private TextMeshProUGUI _difficultyText;

        private void Start()
        {
            _difficultyText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (difficultyValue != Difficulty.Default && difficultyValue != PlayerRunInfo.GetRunDifficulty())
            {
                _difficultyText.color = Color.white;
            }
        }

        public void SetRunDifficulty()
        {
            var runDifficulty = gameObject.GetComponent<DifficultyChanger>().difficultyValue;
            
            PlayerRunInfo.SetDifficulty(difficultyValue);
            
            switch (runDifficulty)
            {
                case Difficulty.Easy:
                    _difficultyText.color = Color.green;
                    break;
                case Difficulty.Normal:
                    _difficultyText.color = Color.blue;
                    break;
                case Difficulty.Hellfire:
                    _difficultyText.color = Color.red;
                    break;
            }
        }
    }
}