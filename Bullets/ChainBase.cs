using System.Collections;
using Boss.Camilla;
using Environment;
using UnityEngine;
using Utils;

namespace Bullets
{
    public class ChainBase : Bullet
    {
        protected bool IsMoving;
        public SpawnerType spawnerType = default;
        
        protected IEnumerator ChargeAnimation()
        {
            var position = transform.position;
            var targetPos = spawnerType == SpawnerType.Down 
                ? new Vector3(position.x, position.y + 2, position.z) 
                : new Vector3(position.x, position.y - 2, position.z);
            
            while (transform.position != targetPos) 
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, 3 * Time.deltaTime);
                yield return null;
            }
        }

        protected void OnTriggerExit2D(Collider2D other)
        {
            other.gameObject.IfHasComponent<Border>(component => StartSpeed = 0);
        }
    }
}