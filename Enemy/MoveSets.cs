using System;
using UnityEngine;

namespace Enemy
{
    [Serializable]
    public enum MoveSet
    {
        [InspectorName("Move to exact position")] ToPosition,
        [InspectorName("Move in direction")] ToPoint,
        [InspectorName("Move around point")] MoveAround,
    }
}