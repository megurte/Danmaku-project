using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float startSpeed;
    public Vector2 direction;


    private void FixedUpdate()
    {
        transform.Translate(direction.normalized * startSpeed);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            Destroy(gameObject);

        if (collision.CompareTag("Border"))
            Destroy(gameObject);
    }
}
