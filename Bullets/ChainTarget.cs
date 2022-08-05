using System.Collections;
using Boss.Camilla;
using Environment;
using UnityEngine;
using Utils;

namespace Bullets
{
    public class ChainTarget : ChainBase
    {
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
            
            var targetPosition = UtilsBase.GetNewPlayerPosition();
            
            Direction = UtilsBase.GetDirection(targetPosition, transform.position);

            var degree = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
                
            transform.rotation = Quaternion.AngleAxis(degree, Vector3.forward);
            _isMoving = true;
        }
    }
}