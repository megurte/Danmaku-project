using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirinMove : MonoBehaviour
{
    public float speed;
    public bool isMoving = false;
    
    [Header("Phase 1")]
    public Vector3 position0 = new Vector2(2.76f, 11.51f); //phase 1
    public Vector3 position1 = new Vector2(-11.54f, 12.15f);
    public Vector3 position2 = new Vector2(-6.12f, 9.62f);
    public Vector3 position3 = new Vector2(-5.36f, 14.09f); //ultimate
    
    [Header("Phase 2")]
    public Vector3 position4 = new Vector2(0, 2); //phase 2

    public Vector3 toPosition;

    private void Start()
    {
        /*StartCoroutine(MoveTo(5, new Kirin.KirinPositions().position0));
        StartCoroutine(MoveTo(9, _position2));
        StartCoroutine(MoveTo(14, _position3));*/
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
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);   
    }

    public IEnumerator MoveTo(float waitTime, Vector2 pos)
    {
        yield return new WaitForSeconds(waitTime);
        isMoving = true;
        toPosition = pos;
    }
}
