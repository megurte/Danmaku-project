using UnityEngine;
using Zenject;

namespace Character
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private GameObject hitBoxHighLight;
        [Inject] private PlayerInputService _inputService;
        private PlayerBase _playerBase;
        private float _speedModifier;
        private Vector2 _moveVector;
        private bool _slowMode;

        private void Start()
        {
            _playerBase = GetComponent<PlayerBase>();
            _speedModifier = _playerBase.playerScriptableObject.speed;
            _slowMode = false;
        }

        private void Update()
        {
            SpeedUpdate();

            if (_inputService.IsMovementControlKeysDown())
            {
                MovePlayer();
            }
            else
            {
                _moveVector = Vector2.zero;
            }
        }
        
        private void MovePlayer()
        {
            var moveInput = new Vector2(_inputService.GetHorizontalAxisValue(), _inputService.GetVerticalAxisValue());

            _moveVector = moveInput.normalized * _speedModifier;
            transform.Translate(_moveVector);
        }
        
        private void SpeedUpdate()
        {
            if (_inputService.IsSlowKeyDown() && !_slowMode)
            {
                _speedModifier /= 4;
                hitBoxHighLight.SetActive(true);
                _slowMode = true;
            }

            if (_inputService.IsSlowKeyUp())
            {
                _speedModifier = _playerBase.playerScriptableObject.speed;
                hitBoxHighLight.SetActive(false);
                _slowMode = false;
            }
        }
    }
}