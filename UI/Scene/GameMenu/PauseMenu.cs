using System;
using UnityEngine;

namespace UI.Scene.GameMenu
{
    public class PauseMenu: MonoBehaviour
    {
        public static bool IsPaused;

        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private GameObject backgroundMusicSource;
        
        private AudioSource _backgroundMusic;

        private void Start()
        {
            _backgroundMusic = backgroundMusicSource.GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
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
            SceneTransition.SceneTransition.AsyncSceneLoading("MainMenu");
            Time.timeScale = 1f;
            IsPaused = false;
        }

        public void RestartStage()
        {
            SceneTransition.SceneTransition.AsyncSceneLoading("StageOne");
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