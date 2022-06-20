using Bullets;
using UnityEngine;

[CreateAssetMenu(fileName = "new Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public int maxHp;
    public int counter;
    public float cooldown;
    public GameObject bullet;
    public Spells spell;
    public MoveSet moveSet;
    public float speed;
    public Vector3 targetPosition;
    public GameObject drop;
}

public enum Spells
{
    DirectTarget,
    Circle,
    RandomShooting,
}

public enum MoveSet
{
    ToPosition,
}