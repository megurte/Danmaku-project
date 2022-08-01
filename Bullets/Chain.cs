using System;
using System.Collections;
using Enemy;
using Environment;
using UnityEngine;
using Utils;

namespace Bullets
{
    public class Chain: Bullet
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
            
            direction = UtilsBase.GetDirection(targetPosition, transform.position);

            var degree = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                
            transform.rotation = Quaternion.AngleAxis(degree, Vector3.forward);
            _isMoving = true;
        }
        
        private IEnumerator ChargeAnimation()
        {
            var position = transform.position;
            var targetPos = new Vector3(position.x, position.y - 4, position.z);
            
            while (transform.position != targetPos) 
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, 3 * Time.deltaTime);
                yield return null;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            other.gameObject.IfHasComponent<Border>(component => startSpeed = 0);
        }
    }
}