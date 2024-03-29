﻿using Character;
using Stage;
using TMPro;
using UI.Scene.Additional;
using UI.Scene.GameMenu;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace UI.MainMenu
{
    public class NewGameWindow: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameInputText;
        [SerializeField] private Button startButton;
        [SerializeField] private string playerName;

        public void LateUpdate()
        {
            CheckStartButtonAvailability();
        }

        private void CheckStartButtonAvailability()
        {
            if (PlayerRunInfo.GetRunDifficulty() != Difficulty.Default && startButton.interactable == false)
            {
                startButton.interactable = true;
                startButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            }
        }

        public void ReadInputField()
        {
            playerName = nameInputText.text;
        }

        public void StartGame()
        {
            if (string.IsNullOrEmpty(playerName))
            {
                playerName = "Unknown";
            }

            if (PlayerRunInfo.GetRunDifficulty() == Difficulty.Default)
            {
                Debug.LogWarning("Select difficulty!");
                return;
            }
            
            PlayerRunInfo.SetPlayerName(playerName);
            SceneTransition.AsyncSceneLoading(Scenes.StageOne.ToString());
        }
    }
}