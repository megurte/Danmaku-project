using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character_controller : MonoBehaviour
{
    public float Health;
    public float speed;
    public float level;
    public bool isInvulnerable;


    private Rigidbody2D _rigidBody;
    private Vector2 _moveVector;

    public GameObject bullet;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Moving();

        if (!isInvulnerable && Health <= 0)
            Destroy(gameObject);
        
        if (Input.GetKey(KeyCode.Space))
            Shoot(level);

        if (Input.GetKey(KeyCode.Q))
            level = 4;
    }

    private void Moving()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _moveVector = moveInput.normalized * speed;
        _rigidBody.velocity = _moveVector * Time.deltaTime;
        
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W))
            _rigidBody.velocity = new Vector2(0, 0);
    }

    private void Shoot(float characterLevel)
    {
        Vector2 bulletPosition1;
        Vector2 bulletPosition2;
        Vector2 bulletPosition3;
        Vector2 bulletPosition4;

        var positionX = transform.position.x;
        var positionY = transform.position.y;
        
        switch (characterLevel)
        {
            case 1:
                bulletPosition1 = new Vector2(positionX, transform.position.y + 0.3f);
                Instantiate(bullet, bulletPosition1, Quaternion.identity);
                break;
            case 2:
                bulletPosition1 = new Vector2(positionX + 0.3f, positionY + 0.3f);
                bulletPosition2 = new Vector2(positionX - 0.3f, positionY + 0.3f);
                Instantiate(bullet, bulletPosition1, Quaternion.identity);
                Instantiate(bullet, bulletPosition2, Quaternion.identity);
                break;
            case 3:
                ///
                break;
            case 4:
                bulletPosition1 = new Vector2(transform.position.x + 0.3f, positionY + 0.3f);
                bulletPosition2 = new Vector2(positionX - 0.3f, positionY + 0.3f);
                bulletPosition3 = new Vector2(positionX + 0.9f, positionY + 0.3f);
                bulletPosition4 = new Vector2(positionX - 0.9f, positionY + 0.3f);
                Instantiate(bullet, bulletPosition1, Quaternion.identity);
                Instantiate(bullet, bulletPosition2, Quaternion.identity);
                Instantiate(bullet, bulletPosition3, Quaternion.identity);
                Instantiate(bullet, bulletPosition4, Quaternion.identity);
                break;
        }
    }

    private IEnumerator Invulnerable()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("enter");
        isInvulnerable = false;
    }
}
