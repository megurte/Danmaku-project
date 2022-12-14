using UnityEngine;

namespace Drop
{
    [CreateAssetMenu(fileName = "new Drop", menuName = "Scriptable Object/Drop")]
    public class DropScriptableObject : ScriptableObject
    {
        public DropType dropType;
        public int value;
    }
}