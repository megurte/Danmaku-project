using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_controller : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private Vector2 moveVector;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVector = moveInput.normalized * speed;
        transform.Translate(moveVector * Time.deltaTime);
        //rb.MovePosition(rb.position + moveVector * Time.fixedDeltaTime);
    }

    //private void FixedUpdate()
    //{
    //    rb.MovePosition(rb.position + moveVector * Time.fixedDeltaTime);
    //}
}
