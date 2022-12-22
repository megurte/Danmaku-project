using UnityEngine;
using Zenject;

namespace Character
{
    public class PlayerSpecials : MonoBehaviour
    {
        public int SpecialsCount { get; private set; }
        public int MaxSpecials { get; private set; }
        
        [Inject] private PlayerInputService _inputService;
        private PlayerBase _playerBase;
        private SpecialSettings _specialSettings;
        private float _specialCooldown;
        private float _specialTimer;

        private void Start()
        {
            _playerBase = GetComponent<PlayerBase>();
            // TODO: ability to choose special before run
            _specialSettings = _playerBase.playerScriptableObject.specialSettings[0];
            _specialCooldown = _specialSettings.specialCooldown;
            _specialTimer = 0;
            SpecialsCount = _specialSettings.special;
            MaxSpecials = _specialSettings.maxSpecials;
        }
        
        private void Update()
        {
            if (_inputService.IsSpecialKeyPressed())
                UseSpecial();

            if (_specialTimer - _specialCooldown < 0)
                _specialTimer -= Time.deltaTime;
        }
        
        private void UseSpecial()
        {
            if (!(_specialTimer <= 0) || SpecialsCount <= 0) return;
            
            SpecialsCount--;
            _specialTimer = _specialCooldown;
            _specialTimer -= Time.deltaTime;
            
            PlayerBase.SpecialUsed.Invoke(SpecialsCount);
            Instantiate(_specialSettings.specialGameObject, _specialSettings.specialPosition, Quaternion.identity);
        }

        public void AddSpecial(int value)
        {
            SpecialsCount += SpecialsCount + value <= MaxSpecials ? value : 0;
        }
    }
}