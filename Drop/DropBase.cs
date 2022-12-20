using System.Collections;
using Character;
using Environment;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Drop
{
    public class DropBase: MonoBehaviour
    {
        [FormerlySerializedAs("dropSo")] public DropScriptableObject dropScriptableObject;

        private int Value => dropScriptableObject.value;
        private DropType DropType => dropScriptableObject.dropType;
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
                PlayerBase.OnGetDrop.Invoke(DropType, Value);
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