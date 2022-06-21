using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Drop", menuName = "Drop")]
public class DropSO : ScriptableObject
{
    public DropType dropType;
    public int value;
}

[Serializable]
public enum DropType
{
    ExpDrop,
    PointDrop,
    HealthDrop,
    SpecialDrop
}