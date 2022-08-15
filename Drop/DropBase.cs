using System.Collections;
using Character;
using Environment;
using UnityEngine;
using Utils;

namespace Drop
{
    public class DropBase: MonoBehaviour
    {
        public DropSO dropSo;

        private int Value => dropSo.value;
        
        private DropType DropType => dropSo.dropType;

        private const float Speed = 3f;

        private void Start()
        {
            StartCoroutine(DropFalling());
        }

        public void AttractToPlayer()
        {
            StopAllCoroutines();
            StartCoroutine(FollowPlayer());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.gameObject.IfHasComponent<PlayerBase>(component =>
            {
                PlayerBase.GetDrop(DropType, Value);
                Destroy(gameObject);
            });
            
            other.gameObject.IfHasComponent<Border>(component => Destroy(gameObject));
        }

        private IEnumerator DropFalling()
        {
            var position = transform.position;
            var targetPos = new Vector3(position.x, position.y + 4, position.z);

            for (float i = 0; i < 2f; i += Time.deltaTime)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, 
                    i / 100);
                yield return null;
            }

            transform.position = targetPos;

            if (transform.position == targetPos)
            {
                StartCoroutine(Fall());
            }
        }

        private IEnumerator Fall()
        {
            while (true)
            {
                var position = transform.position;
                var newTargetPos = new Vector3(position.x, position.y - 20, position.z);
                
                transform.position = Vector3.MoveTowards(transform.position, newTargetPos, Speed 
                    * Time.deltaTime);
                yield return null;
            }
        }

        private IEnumerator FollowPlayer()
        {
            while (transform.position != UtilsBase.GetNewPlayerPosition())
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    UtilsBase.GetNewPlayerPosition(), Speed * 7 * Time.deltaTime);
                yield return null;
            }
        }

        private float EasingInverseSquared(float x)
        {
            return 1 - (1 - x) * (1 - x);
        }
    }
}