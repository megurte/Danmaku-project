using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Scene.GameMenu
{
    public class GameMenuButtons: MonoBehaviour
    {
        public void ReturnToTitle()
        {            
            SceneManager.LoadScene("MainMenu");
        }
    }
}