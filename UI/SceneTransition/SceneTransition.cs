using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.SceneTransition
{
    public class SceneTransition: MonoBehaviour
    {
        [SerializeField] 
        private Image progressBar;

        
        private static SceneTransition instance;
        private static bool isSceneOpeningAnimation = false;

        private Animator _animator;
        private AsyncOperation _asyncOperation;

        public static void AsyncSceneLoading(string sceneName)
        {
            instance._animator.SetTrigger("Closing");
            instance._asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            instance._asyncOperation.allowSceneActivation = false;
        }

        private void Start()
        {
            instance = this;

            _animator = GetComponent<Animator>();

            if (isSceneOpeningAnimation) _animator.SetTrigger("Opening");
        }
        
        private void Update()
        {
            if (_asyncOperation != null) progressBar.fillAmount = _asyncOperation.progress;
        }

        public void OnAnimationOver()
        {
            isSceneOpeningAnimation = true;
            instance._asyncOperation.allowSceneActivation = true;
        }
    }
}