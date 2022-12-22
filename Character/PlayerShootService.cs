using ObjectPool;
using UnityEngine;
using Zenject;

namespace Character
{
    public class PlayerShootService : MonoBehaviour
    {
        [Inject] private PlayerInputService _inputService;
        private PlayerBase _playerBase;
        private float _targetBulletFrequency;
        private float _innerTimer;
        
        private void Start()
        {
            _playerBase = GetComponent<PlayerBase>();
            _targetBulletFrequency = _playerBase.playerScriptableObject.targetBulletFrequency;
            _innerTimer = _targetBulletFrequency;
        }

        private void FixedUpdate()
        {
            if (_inputService.IsShootKeyPressed())
            {
                ShootCommon(_playerBase.Level, transform.position);
            
                if (_playerBase.Level >= 3)
                {
                    var value =
                        ShootTarget(_playerBase.Level, transform.position, _innerTimer, _targetBulletFrequency);
                    _innerTimer = value != 0 ? value : _innerTimer;
                    _innerTimer -= Time.deltaTime;
                }
            }
        }

        public void ShootCommon(int characterLevel, Vector3 position)
        {
            switch (characterLevel)
            {
                case 1:
                    CreateShoot(ObjectPoolTags.PlayerBullet, position, 0, 0.3f);
                    break;
                case 2:
                    CreateShoot(ObjectPoolTags.PlayerBullet, position, 0.3f, 0.3f);
                    CreateShoot(ObjectPoolTags.PlayerBullet, position, 0.3f, 0.3f, true);
                    break;
                case 3:
                    CreateShoot(ObjectPoolTags.PlayerBullet, position, 0.3f, 0.3f);
                    CreateShoot(ObjectPoolTags.PlayerBullet, position, 0.3f, 0.3f, true);
                    break;
                case 4:
                    CreateShoot(ObjectPoolTags.PlayerBullet, position, 0.3f, 0.3f);
                    CreateShoot(ObjectPoolTags.PlayerBullet, position, 0.3f, 0.3f, true);
                    break;
            }
        }

        public float ShootTarget(int characterLevel, Vector3 position, float innerTimer, float targetBulletFrequency)
        {
            if (!(innerTimer <= 0)) return 0;

            switch (characterLevel)
            {
                case 3:
                    CreateShoot(ObjectPoolTags.TargetPlayerBullet, position, 1.2f, 0.3f);
                    CreateShoot(ObjectPoolTags.TargetPlayerBullet, position, 1.2f, 0.3f, true);
                    break;
                case 4:
                    CreateShoot(ObjectPoolTags.TargetPlayerBullet, position, 1.2f, 0.3f);
                    CreateShoot(ObjectPoolTags.TargetPlayerBullet, position,1.2f, 0.3f, true);
                    CreateShoot(ObjectPoolTags.TargetPlayerBullet, position,1.8f, 0);
                    CreateShoot(ObjectPoolTags.TargetPlayerBullet, position,1.8f, 0, true);
                    break;
            }
            return targetBulletFrequency;
        }
        
        private void CreateShoot(ObjectPoolTags objectPoolTag, Vector3 position, float xOffset, float yOffset, bool deduction = false)
        {
            Vector3 bulletPosition = deduction 
                ? new Vector2(position.x - xOffset, position.y + yOffset) 
                : new Vector2(position.x + xOffset, position.y + yOffset);
            ObjectPoolBase.GetBulletFromPool(objectPoolTag, bulletPosition);
        }
    }
}