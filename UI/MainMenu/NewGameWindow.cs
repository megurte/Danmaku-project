using System.Net.Mime;
using Character;
using ModestTree;
using Stage;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace UI.MainMenu
{
    public class NewGameWindow: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameInputText;
        [SerializeField] private Button startButton;
        [SerializeField] private string playerName;

        public void Update()
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
            if (playerName.IsEmpty())
            {
                playerName = "Unknown";
            }

            if (PlayerRunInfo.GetRunDifficulty() == Difficulty.Default)
            {
                Debug.LogWarning("Select difficulty!");
                return;
            }
            
            PlayerRunInfo.SetPlayerName(playerName);
            SceneTransition.SceneTransition.AsyncSceneLoading("StageOne");
        }
    }
}