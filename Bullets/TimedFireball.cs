using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedFireball: MonoBehaviour
{
    private const float StartSpeed = 0.1f;
    private bool _isActive = false;
    public Vector2 direction;
    public Vector3 rotation;
    
    private void FixedUpdate()
    {
        if (!_isActive)
        {
            transform.eulerAngles = rotation;
            _isActive = true;
        }
        
        transform.Translate(direction.normalized * Time.timeScale * 0.1f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Destroy(gameObject);
        if (collision.CompareTag("Border"))
            Destroy(gameObject);
    }
}