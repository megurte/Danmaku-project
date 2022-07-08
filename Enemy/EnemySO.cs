using System;
using Bullets;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "new Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public int maxHp;
    public int counter;
    public float cooldown;
    public GameObject bullet;
    public GameObject destroyEffect;
    public Spells spell;
    public MoveSet moveSet;
    public float speed;
    public float angularSpeed;
    public float radius;
    public Vector3 targetPosition;
    public List<LootSettings> lootSettings;
}

[Serializable]
public struct LootSettings
{
    public GameObject dropItem;
    public int dropNumber;
    public float chance;
}

[Serializable]
public enum Spells
{
    [InspectorName("Shoot in target")] DirectTarget,
    [InspectorName("Shoot around object (Circle)")] Circle,
    [InspectorName("Random shoot direction")] RandomShooting,
}

[Serializable]
public enum MoveSet
{
    [InspectorName("Move to exact position")] ToPosition,
    [InspectorName("Move in direction")] ToPoint,
    [InspectorName("Move around point")] MoveAround,
}