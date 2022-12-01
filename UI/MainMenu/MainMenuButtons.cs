using UnityEngine;

namespace UI.MainMenu
{
    public class MainMenuButtons : MonoBehaviour
    {
        public void Play()
        {            
            SceneTransition.SceneTransition.AsyncSceneLoading("StageOne");
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
