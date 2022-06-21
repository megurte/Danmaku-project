using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        public float health;
        public float speed;
        public int level;
        public int exp = default;
        public bool isInvulnerable;
        public GameObject bullet;
        public GameObject targetBullet;
        public float targetBulletFrequency;

        private Rigidbody2D _rigidBody;
        private Vector2 _moveVector;
        private float _innerTimer;
        
        private static UnityEvent<DropType, int> OnGetDrop = new UnityEvent<DropType, int>();

        private void Start()
        {
            _innerTimer = targetBulletFrequency;
            _rigidBody = GetComponent<Rigidbody2D>();
            
            OnGetDrop.AddListener(OnDrop);
        }

        private void FixedUpdate()
        {
            Moving();

            if (!isInvulnerable && health <= 0)
                Destroy(gameObject);

            if (Input.GetKey(KeyCode.Space))
            {
                ShootCommon(level);
            
                if (level >= 3)
                {
                    ShootTarget(level);
                    _innerTimer -= Time.deltaTime;
                }
            }

            if (Input.GetKey(KeyCode.F2))
                level = 2;
            if (Input.GetKey(KeyCode.F3))
                level = 3;
            if (Input.GetKey(KeyCode.F4))
                level = 4;
        }

        private void Moving()
        {
            var moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            _moveVector = moveInput.normalized * speed;
            _rigidBody.velocity = _moveVector * Time.deltaTime;
        
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W))
                _rigidBody.velocity = new Vector2(0, 0);
        }

        private void ShootCommon(int characterLevel)
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
                    bulletPosition1 = new Vector2(positionX + 0.3f, positionY + 0.3f);
                    bulletPosition2 = new Vector2(positionX - 0.3f, positionY + 0.3f);
                    Instantiate(bullet, bulletPosition1, Quaternion.identity);
                    Instantiate(bullet, bulletPosition2, Quaternion.identity);

                    break;
                case 4:
                    bulletPosition1 = new Vector2(transform.position.x + 0.3f, positionY + 0.3f);
                    bulletPosition2 = new Vector2(positionX - 0.3f, positionY + 0.3f);
                    /*bulletPosition3 = new Vector2(positionX + 0.6f, positionY + 0.3f);
                bulletPosition4 = new Vector2(positionX - 0.6f, positionY + 0.3f);*/
                    Instantiate(bullet, bulletPosition1, Quaternion.identity);
                    Instantiate(bullet, bulletPosition2, Quaternion.identity);
                    /*Instantiate(bullet, bulletPosition3, Quaternion.identity);
                Instantiate(bullet, bulletPosition4, Quaternion.identity);*/
                    break;
            }
        }

        private void ShootTarget(int characterLevel)
        {
            if (!(_innerTimer <= 0)) return;
        
            Vector2 targetBulletPosition1;
            Vector2 targetBulletPosition2;
            Vector2 targetBulletPosition3;
            Vector2 targetBulletPosition4;
        
            var positionX = transform.position.x;
            var positionY = transform.position.y;
        
            switch (characterLevel)
            {
                case 3:
                    targetBulletPosition1 = new Vector2(positionX + 1.2f, positionY + 0.3f);
                    targetBulletPosition2 = new Vector2(positionX - 1.2f, positionY + 0.3f);
                    Instantiate(targetBullet, targetBulletPosition1, Quaternion.identity);
                    Instantiate(targetBullet, targetBulletPosition2, Quaternion.identity);
                    break;
                case 4:
                    targetBulletPosition1 = new Vector2(positionX + 1.2f, positionY + 0.3f);
                    targetBulletPosition2 = new Vector2(positionX - 1.2f, positionY + 0.3f);
                    targetBulletPosition3 = new Vector2(positionX + 1.8f, positionY + 0.0f);
                    targetBulletPosition4 = new Vector2(positionX - 1.8f, positionY + 0.0f);
                    Instantiate(targetBullet, targetBulletPosition1, Quaternion.identity);
                    Instantiate(targetBullet, targetBulletPosition2, Quaternion.identity);
                    Instantiate(targetBullet, targetBulletPosition3, Quaternion.identity);
                    Instantiate(targetBullet, targetBulletPosition4, Quaternion.identity);
                    break;
            }
            
            _innerTimer = targetBulletFrequency;
        }

        public static void GetDrop(DropType type, int value)
        {
            OnGetDrop.Invoke(type, value);
        }
        
        private void OnDrop(DropType type, int value)
        {
            switch (type)
            {
                case DropType.ExpDrop:
                    exp += value;
                    break;
                case DropType.PointDrop:
                    ///
                    break;
                case DropType.HealthDrop:
                    ///
                    break;
                case DropType.SpecialDrop:
                    ///
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
}
