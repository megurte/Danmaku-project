using UnityEngine;

namespace Boss.Camilla
{
    [CreateAssetMenu(fileName = "new Camilla", menuName = "Camilla")]
    public class CamillaSO: ScriptableObject
    {
        public float maxHp;
        public float lerpSpeed;
    }
}