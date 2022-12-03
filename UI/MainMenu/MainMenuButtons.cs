using System;
using UnityEngine;

namespace UI.MainMenu
{
    public class MainMenuButtons : MonoBehaviour
    {
        [SerializeField] private GameObject recordsPanel;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (recordsPanel)
                {
                    recordsPanel.SetActive(false);
                }
            }
        }

        public void Play()
        {            
            SceneTransition.SceneTransition.AsyncSceneLoading("StageOne");
        }
        
        public void ShowRecords()
        {
            recordsPanel.SetActive(true);
            GetComponent<RecordsWindow>().UpdateRecordsData();
        }
        
        public void ExitApplication()
        {
            Application.Quit();
        }
    }
}
