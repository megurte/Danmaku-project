using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.MainMenu
{
    public class MainMenuButtons : MonoBehaviour
    {
        [SerializeField] private GameObject recordsWindow;
        [SerializeField] private GameObject newGameWindow;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (recordsWindow)
                {
                    recordsWindow.SetActive(false);
                }

                if (newGameWindow)
                {
                    newGameWindow.SetActive(false);
                }
            }
        }

        public void Play()
        {            
            newGameWindow.SetActive(true);
        }
        
        public void ShowRecords()
        {
            recordsWindow.SetActive(true);
            GetComponent<RecordsWindow>().UpdateRecordsData();
        }
        
        public void ExitApplication()
        {
            Application.Quit();
        }
    }
}
