using System;
using UnityEngine;

namespace Enemy
{
    [Serializable]
    public enum Spells
    {
        [InspectorName("Shoot in target")] DirectTarget,
        [InspectorName("Shoot around object (Circle)")] Circle,
        [InspectorName("Random shoot direction")] RandomShooting,
    }
}