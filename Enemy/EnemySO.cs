using System;
using Bullets;
using System.Collections.Generic;
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
    public LootSettings lootSettings;

}

[Serializable]
public struct LootSettings
{
    public DropSettings expSettings;
    public DropSettings pointSettings;
    public DropSettings healthSettings;
    public DropSettings specialSettings;
}

[Serializable]
public struct DropSettings
{
    public GameObject dropItem;
    public int dropNumber;
    public float chance;
}

[Serializable]
public enum Spells
{
    DirectTarget,
    Circle,
    RandomShooting,
}

[Serializable]
public enum MoveSet
{
    ToPosition,
    ToPoint,
    MoveAround,
}