using System;
using System.Collections;
using Character;
using Common;
using Environment;
using UnityEngine;
using Utils;
using Zenject;

namespace Drop
{
    public class DropBase: MonoBehaviour
    {
        [SerializeField] private DropScriptableObject dropScriptableObject;
        [Inject] private PlayerBase _player;
        private int Value => dropScriptableObject.value;
        private DropType DropType => dropScriptableObject.dropType;
        private float _followSpeed = 3f;
        private Vector3 _moveVector;
        private bool _isAttracted;
        
        protected void Awake()
        {
            if (gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                gameObject.layer = (int)Layers.DropLayer;
            }

            _moveVector = new Vector3(0, -0.08f, 0);
            _isAttracted = false;
        }

        private void FixedUpdate()
        {
            if (!_isAttracted)
            {
                transform.Translate(_moveVector);
            }
        }

        public void AttractToPlayer()
        {
            _isAttracted = true;
            StopAllCoroutines();
            StartCoroutine(FollowPlayer());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((other.gameObject.layer & (int)Layers.BulletLayer) != 0 
                || (other.gameObject.layer & (int)Layers.DropLayer) != 0)
            {
                return;
            }
            
            if (other.gameObject == _player.gameObject)
            {
                PlayerBase.OnGetDrop.Invoke(DropType, Value);
                Destroy(gameObject);
            }

            other.gameObject.HasComponent<Border>(component => Destroy(gameObject));
        }

        private IEnumerator FollowPlayer()
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            
            while (transform.position != _player.GetPlayerPosition())
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    _player.GetPlayerPosition(), _followSpeed * 7 * Time.deltaTime);
                yield return null;
            }
        }
    }
}