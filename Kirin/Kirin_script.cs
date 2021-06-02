using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirin_script : MonoBehaviour
{
    public GameObject fireball;
    private GameObject inst_obj;

    //fireball spawn
    public float distance = 2;
    public float angle = 360;
    public int count = 15;


    void Start()
    {
        StartCoroutine(WairForCircle(3, true));
        StartCoroutine(WairForCircle(4, false));
        StartCoroutine(WairForCircle(5, true));
        StartCoroutine(WairForCircle(6, false));
        StartCoroutine(WairForCircle(7, true));
        StartCoroutine(WaitForLeft(8, true));
        StartCoroutine(WaitForLeft(9, false));
        StartCoroutine(WaitForLeft(10, true));
        StartCoroutine(WaitForLeft(11, false));
        StartCoroutine(WairForCircle(12, false));
        StartCoroutine(WairForCircle(13, true));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Fireball_spell_leftright(bool change)
    {

        Vector2 point = transform.position;
        Vector2 direction = new Vector2(-1, 1);

        angle = angle * Mathf.Deg2Rad;
        for (int i = 1; i <= count; i++)
        {
            float _y = transform.position.y + Mathf.Cos(angle / count * i) * distance;
            float _x = transform.position.x + Mathf.Sin(angle / count * i) * distance;
            point.x = _x;
            point.y = _y;

            float dir_y = direction.y + Mathf.Cos(angle / count * i);
            float dir_x = direction.x + Mathf.Sin(angle / count * i);
            direction.x = dir_x;
            direction.y = dir_y;
            if (change)
                Fireball_spawn(point, direction, true);
            else
                Fireball_spawn(point, direction, false);
        }
        angle = 360;
    }

    void Fireball_spell_circle(bool change)
    {

        Vector2 point = transform.position;
        Vector2 direction = new Vector2(-1,1);

        angle = angle * Mathf.Deg2Rad;
        for (int i = 1; i <= count; i++)
        {
            float _y = transform.position.y + Mathf.Cos(angle / count * i) * distance;
            float _x = transform.position.x + Mathf.Sin(angle / count * i) * distance;
            point.x = _x;
            point.y = _y;

            float dir_y = Mathf.Cos(angle / count * i);
            float dir_x = Mathf.Sin(angle / count * i);
            direction.x = dir_x;
            direction.y = dir_y;
            if(change)
                Fireball_spawn(point, direction, true);
            else
                Fireball_spawn(point, direction, false);
        }
        angle = 360;          
    }

    void Fireball_spawn(Vector2 pos, Vector2 dir,bool left_right)
    {
        inst_obj = Instantiate(fireball, pos, Quaternion.identity);
        if(left_right)
            inst_obj.GetComponent<Fireball>().direction = dir;
        else
            inst_obj.GetComponent<Fireball>().direction = -dir;
    }

    IEnumerator WairForCircle(float wait_time, bool change)
    {
        yield return new WaitForSeconds(wait_time);
        Fireball_spell_circle(change);
    }

    IEnumerator WaitForLeft(float wait_time, bool change)
    {
        yield return new WaitForSeconds(wait_time);
        Fireball_spell_leftright(change);
    }
}
