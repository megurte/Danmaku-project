using System;
using System.Collections;
using Bullets;
using UnityEngine;

public abstract class KirinSpells : MonoBehaviour
{
    //Bullets
    public GameObject fireball;
    public GameObject fireBullet;
    public GameObject timedFireball;
    public GameObject fireballSmall;
    public GameObject icicle;

    //Bullet spawn params
    private GameObject _instObject;
    private const float FullDegrees = 360;
    public float distance = 2;
    public float angle = 360;
   
    protected void FireballSpellLeftToRight(bool change, GameObject bullet, int count)
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

    protected void FireballSpellCircle(bool change, GameObject bullet, int count)
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
    protected void IcicleSpellCircle(bool change, GameObject bullet, int count)
    {
        var point = transform.position;
        var direction = new Vector2(-1, 1);

        angle *= Mathf.Deg2Rad;
        for (int i = 1; i <= count; i++)
        {
            var y = transform.position.y + Mathf.Cos(angle / count * i) * distance;
            var x = transform.position.x + Mathf.Sin(angle / count * i) * distance;
            point.x = x;
            point.y = y;

            var dirY = Mathf.Cos(angle / count * i);
            var dirX = Mathf.Sin(angle / count * i);
            direction.x = dirX;
            direction.y = dirY;

                            

            if (change)
                BulletSpawn(point, direction, angle / count * i,true, bullet);
            else
                BulletSpawn(point, direction, angle / count * i, false, bullet);
        }
        angle = FullDegrees;
    }
    
    
    private void BulletSpawn(Vector2 pos, Vector2 dir, float degree, bool leftToRight, GameObject bullet)
    {
        _instObject = Instantiate(bullet, pos, Quaternion.identity);
        _instObject.GetComponent<Bullet>().angle = degree;
        if (leftToRight)
            _instObject.GetComponent<Bullet>().direction = dir;
        else
            _instObject.GetComponent<Bullet>().direction = -dir;
    }

    protected IEnumerator SpiralSpellFireball(float delay, bool change, GameObject bullet, float count)
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
    
    protected IEnumerator RouletteSpellFireball(float delay, bool change, GameObject bullet, float count)
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
