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
        private float _speed = 3f;

        public void AttractToPlayer()
        {
            StopAllCoroutines();
            StartCoroutine(FollowPlayer());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.gameObject.HasComponent<PlayerBase>(component =>
            {
                PlayerBase.GetDrop(DropType, Value);
                Destroy(gameObject);
            });
            
            other.gameObject.HasComponent<Border>(component => Destroy(gameObject));
        }

        private IEnumerator FollowPlayer()
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            
            while (transform.position != UtilsBase.GetNewPlayerPosition())
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    UtilsBase.GetNewPlayerPosition(), _speed * 7 * Time.deltaTime);
                yield return null;
            }
        }
    }
}