using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirin_move : MonoBehaviour
{
    public float speed;
    public bool isMoving = false;

    private readonly Vector3 _position0 = new Vector2(-10.9f, -8.3f); //phase 1
    private readonly Vector3 _position1 = new Vector2(-11.56f, 12.15f);
    private readonly Vector3 _position2 = new Vector2(-0.14f, 9.93f);
    private readonly Vector3 _position3 = new Vector2(-5.36f, 14.09f); //ultimate
    private readonly Vector3 _position4 = new Vector2(0, 2); //phase 2

    public Vector3 toPosition;

    private void Start()
    {
        StartCoroutine(WaitToMove(5, _position1));
        StartCoroutine(WaitToMove(9, _position2));
        StartCoroutine(WaitToMove(14, _position3));
    }

    private void Update()
    {
        if (isMoving)
            MovementToPosition(toPosition);

        if (transform.position == toPosition)
            isMoving = false;
    }

    private void MovementToPosition(Vector3 targetPos)
    {    
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);   
    }

    private IEnumerator WaitToMove(float waitTime, Vector2 pos)
    {
        yield return new WaitForSeconds(waitTime);
        isMoving = true;
        toPosition = pos;
    }
}
