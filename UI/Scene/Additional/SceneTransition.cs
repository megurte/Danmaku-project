using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Scene.Additional
{
    public class SceneTransition: MonoBehaviour
    {
        [SerializeField] 
        private RectTransform arrowPivot;

        private Quaternion _angle;

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

            _angle = arrowPivot.rotation;
        }
        
        private void Update()
        {
            if (_asyncOperation == null) return;

            if (_angle != arrowPivot.rotation || arrowPivot.rotation.z == 0)
            {
                arrowPivot.Rotate(new Vector3(0, 0, _asyncOperation.progress * 360f));
                _angle = arrowPivot.rotation;
            }
        }

        public void OnAnimationOver()
        {
            isSceneOpeningAnimation = true;
            instance._asyncOperation.allowSceneActivation = true;
        }
    }
}