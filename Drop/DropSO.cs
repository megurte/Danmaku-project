using UnityEngine;

namespace Drop
{
    [CreateAssetMenu(fileName = "new Drop", menuName = "Scriptable Object/Drop")]
    public class DropSO : ScriptableObject
    {
        public DropType dropType;
        public int value;
    }
}