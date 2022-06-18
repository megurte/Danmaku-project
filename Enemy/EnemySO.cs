using Bullets;
using UnityEngine;

[CreateAssetMenu(fileName = "new Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public int maxHp;
    public int counter;
    public float cooldown;
    public GameObject bullet;
    public float speed;
    public Vector3 targetPosition;
}
