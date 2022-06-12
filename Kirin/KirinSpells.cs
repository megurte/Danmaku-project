using System;
using System.Collections;
using System.Runtime;
using System.Collections.Generic;
using UnityEngine;

public struct FireballCircle
{
    public float Time;
    public bool Change;
    public GameObject Bullet;
    public int Count;
}

public class KirinSpells : MonoBehaviour
{
    //Bullets
    public GameObject fireball;
    public GameObject fireBullet;
    public GameObject timedFireball;
    public GameObject fireballSmall;
    public GameObject icicle;

    //Bullet spawn
    private GameObject InstObject;
    private const float FullDegrees = 360;
    public float distance = 2;
    public float angle = 360;
   
    private void FireballSpellLeftToRight(bool change, GameObject bullet, int count)
    {
        Vector2 point = transform.position;
        var direction = new Vector2(-1, 1);

        angle *= Mathf.Deg2Rad;
        for (var i = 1; i <= count; i++)
        {
            var degree = angle / count * i;
            var y = transform.position.y + Mathf.Cos(degree) * distance;
            var x = transform.position.x + Mathf.Sin(degree) * distance;
            point.x = x;
            point.y = y;

            var dirY = direction.y + Mathf.Cos(degree);
            var dirX = direction.x + Mathf.Sin(degree);
            direction.x = dirX;
            direction.y = dirY;
            BulletSpawn(point, direction, degree * Mathf.Rad2Deg, change, bullet);
        }
        angle = FullDegrees;
    }

    private void FireballSpellCircle(bool change, GameObject bullet, int count)
    {
        Vector2 point = transform.position;
        var direction = new Vector2(-1, 1);

        angle *= Mathf.Deg2Rad;
        for (var i = 1; i <= count; i++)
        {
            var degree = angle / count * i;
            var y = transform.position.y + Mathf.Cos(degree) * distance;
            var x = transform.position.x + Mathf.Sin(degree) * distance;
            point.x = x;
            point.y = y;

            var dirY = Mathf.Cos(degree);
            var dirX = Mathf.Sin(degree);
            direction.x = dirX;
            direction.y = dirY;

            BulletSpawn(point, direction,degree * Mathf.Rad2Deg, change, bullet);
        }
        angle = FullDegrees;           
    }

    //TEST
    private void IcicleSpellCircle(bool change, GameObject bullet, int count)
    {
        var point = transform.position;
        var direction = new Vector2(-1, 1);

        angle *= Mathf.Deg2Rad;
        for (int i = 1; i <= count; i++)
        {
            var _y = transform.position.y + Mathf.Cos(angle / count * i) * distance;
            var _x = transform.position.x + Mathf.Sin(angle / count * i) * distance;
            point.x = _x;
            point.y = _y;

            var dir_y = Mathf.Cos(angle / count * i);
            var dir_x = Mathf.Sin(angle / count * i);
            direction.x = dir_x;
            direction.y = dir_y;

                            

            if (change)
                BulletSpawn(point, direction, angle / count * i,true, bullet);
            else
                BulletSpawn(point, direction, angle / count * i, false, bullet);
        }
        angle = FullDegrees;
    }
    
    
    private void BulletSpawn(Vector2 pos, Vector2 dir, float degree, bool leftToRight, GameObject bullet)
    {
        InstObject = Instantiate(bullet, pos, Quaternion.identity);
        InstObject.GetComponent<Fireball>().angle = degree;
        if (leftToRight)
            InstObject.GetComponent<Fireball>().direction = dir;
        else
            InstObject.GetComponent<Fireball>().direction = -dir;
    }

    public IEnumerator SpiralSpellFireball(float delay, bool change, GameObject bullet, float count)
    {
        Vector2 point = transform.position;
        var direction = new Vector2(-1, 1);

        angle *= Mathf.Deg2Rad;
        for (var i = 1; i <= count; i++)
        {
            var degree = angle / count * i;
            var y = transform.position.y + Mathf.Cos(degree) * distance;
            var x = transform.position.x + Mathf.Sin(degree) * distance;
            point.x = x;
            point.y = y;

            var dirY = Mathf.Cos(degree);
            var dirX = Mathf.Sin(degree);
            direction.x = dirX;
            direction.y = dirY;
            yield return new WaitForSeconds(delay);
            BulletSpawn(point, direction, degree * Mathf.Rad2Deg, change, bullet);
        }
        angle = FullDegrees;
    }
    
    public IEnumerator RouletteSpellFireball(float delay, bool change, GameObject bullet, float count)
    {
        Vector2 point = transform.position;
        var direction = new Vector2(0, -1);

        angle *= Mathf.Deg2Rad;
        for (var i = 1; i <= count; i++)
        {
            var degree = angle / count * i;
            var y = transform.position.y + Mathf.Cos(degree) * distance;
            var x = transform.position.x + Mathf.Sin(degree) * distance;
            point.x = x;
            point.y = y;

            var dirY = Mathf.Cos(degree);
            var dirX = Mathf.Sin(degree);
            direction.x = dirX;
            direction.y = dirY;
            yield return new WaitForSeconds(delay);
            BulletSpawn(point, direction, degree * Mathf.Rad2Deg, change, bullet);
        }
        angle = FullDegrees;
    }
    
    public IEnumerator RouletteFireball(float waitTime, bool change, GameObject bullet, float count, float delay)
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(RouletteSpellFireball(delay, change, bullet, count));
    }
    
    public IEnumerator SpiralFireball(float waitTime, bool change, GameObject bullet, float count, float delay)
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(SpiralSpellFireball(delay, change, bullet, count));
    }
    
    public IEnumerator CircleIcicle(float waitTime, bool change, GameObject bullet, int count)
    {
        yield return new WaitForSeconds(waitTime);
        IcicleSpellCircle(change, bullet, count);
    }

    public IEnumerator CircleFireball(float waitTime, bool change, GameObject bullet, int count)
    {
        yield return new WaitForSeconds(waitTime);
        FireballSpellCircle(change, bullet, count);
    }

    public IEnumerator LeftSideFireball(float waitTime, bool change, GameObject bullet, int count)
    {
        yield return new WaitForSeconds(waitTime);
        FireballSpellLeftToRight(change, bullet, count);
    }
    
    /*public IEnumerator CircleFireballSpell(CircleFireballSettings settings)
    {
        yield return new WaitForSeconds(settings.waitTime);
        FireballSpellCircle(settings.change, settings.bullet, settings.count);
    }*/
}

/*public class CircleFireballSettings
{
    public float waitTime;
    public bool change;
    public GameObject bullet;
    public int count;
}*/
