using Common;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace UI.MainMenu
{
    public class MainMenuButtons : MonoBehaviour
    {
        public void Play()
        {            
            SceneManager.LoadScene("StageOne");
            Debug.Log("Play");
        }
        
        public void ShowRecords()
        {
            Debug.Log("Records");
        }
        
        public void ExitApplication()
        {
            Application.Quit();
        }
    }
}
