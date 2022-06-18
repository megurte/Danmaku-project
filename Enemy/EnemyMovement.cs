using System;
using System.Collections;
using System.Collections.Generic;
using Bullets;
using DefaultNamespace.Spells;
using UnityEngine;
using Random = System.Random;

public class EnemyMovement : MonoBehaviour
{
    public EnemySO enemySo;
    public Vector3 targetPosition;
    private GameObject bulletPrefab;
    private float speed;
    private Vector3 _playersPosition;
    private Vector3 _direction;
    private bool _isCharging = false;
    private bool _isCharged = false;

    private void Awake()
    {
        bulletPrefab = enemySo.bullet;
        speed = enemySo.speed;
    }

    private void FixedUpdate()
    {

        CommonSpells.RandomShooting(bulletPrefab, transform.position, 1);
        
        if (!_isCharging)
            MovementToPosition(targetPosition);


        if (_isCharged)
            MoveToDirection(_direction);

        if (gameObject.transform.position == targetPosition && !_isCharging)
        {
            _isCharging = true;
            StartCoroutine(Charge());
        }
    }

    private void MovementToPosition(Vector3 targetPos)
    {    
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);   
    }
    
    private void MoveToDirection(Vector3 direction)
    {
        transform.Translate(direction.normalized * speed, Space.World);
    }

    private Vector3 GetNewTargetPosition()
    {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    private void BulletSpawnBeforeDeath()
    {
        var rnd = new Random();

        for (var i = 0; i < 12; i++)
        {
            _playersPosition = GetNewTargetPosition();
            
            var position = transform.position;
            var newBullet = Instantiate(bulletPrefab, new Vector3(
                position.x + rnd.Next(0, 5),
                position.y + rnd.Next(0, 3), position.z), 
                Quaternion.Euler(_playersPosition));

            newBullet.GetComponent<Bullet>().direction = GetDirection(_playersPosition, 
                newBullet.transform.position);

        }
    }

    private Vector3 GetDirection(Vector3 targetPos, Vector3 objectPosition)
    {
        var heading = targetPos - objectPosition;
        var distance = heading.magnitude;

        return heading / distance;
    }

    private IEnumerator Charge()
    {
        yield return new WaitForSeconds(3);
        speed = 0.5f;
        _isCharged = true;
        targetPosition = GetNewTargetPosition();
        _direction = GetDirection(targetPosition, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterController>().health -= 1;
            //collision.GetComponent<Character_controller>().isInvulnerable = true;
        }

        if (other.CompareTag("Border"))
        {
            BulletSpawnBeforeDeath();
            Destroy(gameObject);
        }

    }
}
