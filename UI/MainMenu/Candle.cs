using System;
using UnityEngine;

namespace UI.MainMenu
{
    public class Candle: MonoBehaviour
    {
        [SerializeField] private float mouseSpeed;
        [SerializeField] private Camera mainCamera;
        private Rigidbody2D _rigidbody2D;
        private Vector3 _mousePosition;
        private Vector2 _position = new Vector2(0f, 0f);

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _mousePosition = Input.mousePosition;
            _mousePosition = mainCamera.ScreenToWorldPoint(_mousePosition);
            _position = Vector2.Lerp(transform.position, _mousePosition, mouseSpeed);
        }
        
        private void FixedUpdate()
        {
            _rigidbody2D.MovePosition(_position);
        }
    }
}