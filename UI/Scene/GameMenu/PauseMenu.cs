using System;
using Character;
using UI.Scene;
using UI.Scene.Additional;
using UnityEngine;
using Zenject;

namespace UI.Scene.GameMenu
{
    public class PauseMenu: MonoBehaviour
    {
        public static bool IsPaused;

        [Inject] private PlayerInputService _inputService;
        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private GameObject backgroundMusicSource;
        
        private AudioSource _backgroundMusic;

        private void Start()
        {
            _backgroundMusic = backgroundMusicSource.GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_inputService.IsReturnKeyPressed())
            {
                if (IsPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        public void ResumeGame()
        {
            pauseMenuUI.SetActive(false);
            _backgroundMusic.volume = 0.5f;
            Time.timeScale = 1f;
            IsPaused = false;
        }

        public void LoadMenu()
        {
            SceneTransition.AsyncSceneLoading(Scenes.MainMenu.ToString());
            Time.timeScale = 1f;
            IsPaused = false;
        }

        public void RestartStage()
        {
            SceneTransition.AsyncSceneLoading(Scenes.StageOne.ToString());
            Time.timeScale = 1f;
            IsPaused = false;
        }

        private void PauseGame()
        {
            pauseMenuUI.SetActive(true);
            _backgroundMusic.volume = 0.05f;
            Time.timeScale = 0f;
            IsPaused = true;
        }
    }
}