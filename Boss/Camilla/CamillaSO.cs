using System.Collections.Generic;
using UnityEngine;

namespace Boss.Camilla
{
    [CreateAssetMenu(fileName = "new Camilla", menuName = "Camilla")]
    public class CamillaSO: ScriptableObject
    {
        public float maxHp;
        public float lerpSpeed;

        public List<GameObject> bullets = new List<GameObject>();
    }
}