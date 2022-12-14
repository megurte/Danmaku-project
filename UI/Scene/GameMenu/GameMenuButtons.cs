using UI.Scene.Additional;
using UnityEngine;

namespace UI.Scene.GameMenu
{
    public class GameMenuButtons: MonoBehaviour
    {
        public void ReturnToTitle()
        {            
            SceneTransition.AsyncSceneLoading("MainMenu");
        }
    }
}