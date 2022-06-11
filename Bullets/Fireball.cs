using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float startSpeed = 0.1f;
    private bool _isActive = false;
    public string bulletType = "Bullet";
    public Vector3 direction;
    public float angle = 180;
    public Vector3 rotation;
    
    private void FixedUpdate()
    {
        /*if (!_isActive)
        {
            transform.eulerAngles = rotation;
            //_isActive = true;
        }*/
        
        transform.Translate(direction.normalized * startSpeed, Space.World);

        if (bulletType == "Fire")
        {
            //var position = transform.position;
            //transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), Time.deltaTime * 40f);
            var degree = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(degree, Vector3.forward);

            //transform.rotation = Quaternion.Euler(0,0,angle);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<CharacterController>().health -= 1;
            //collision.GetComponent<Character_controller>().isInvulnerable = true;
            Destroy(gameObject);
        }
            
        if (collision.CompareTag("Border"))
            Destroy(gameObject);
    }
}
