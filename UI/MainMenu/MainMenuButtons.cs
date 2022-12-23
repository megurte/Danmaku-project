using System;
using UI.Scene.Inscriptions;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.MainMenu
{
    public class MainMenuButtons : MonoBehaviour
    {
        [SerializeField] private GameObject recordsWindow;
        [SerializeField] private GameObject newGameWindow;
        [SerializeField, TextArea(5,7)] private string controlsDescription;

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
        
        public void ShowGameControls()
        {
            TextDisplay.DisplayContent.Invoke("", controlsDescription);
        }
        
        public void ExitApplication()
        {
            Application.Quit();
        }
    }
}
