using System.Collections;
using Boss.Camilla;
using Character;
using Environment;
using Interfaces;
using UnityEngine;
using Utils;
using Zenject;

namespace Bullets
{
    public class ChainTarget : ChainBase
    {
        [Inject] private PlayerBase _playerBase;
        private bool _isMoving;

        private void Start()
        {
            StartCoroutine(ChainMove());
        }
        
        private void FixedUpdate()
        {
            if (_isMoving)
                Moving();
        }

        private IEnumerator ChainMove()
        {
            yield return StartCoroutine(ChargeAnimation());
            yield return new WaitForSeconds(1.5f);
            
            var targetPosition = _playerBase.GetPlayerPosition();
            
            Direction = UtilsBase.GetDirection(targetPosition, transform.position);

            var degree = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
                
            transform.rotation = Quaternion.AngleAxis(degree, Vector3.forward);
            _isMoving = true;
        }
    }
}