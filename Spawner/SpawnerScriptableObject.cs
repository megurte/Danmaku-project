using System.Collections.Generic;
using UnityEngine;

namespace Spawner
{
    [CreateAssetMenu(fileName = "new Spawner", menuName = "Scriptable Object/Spawner")]
    public class SpawnerScriptableObject : ScriptableObject
    {
        public List<EnemySpawnProperties> enemySpawnPropertiesList;
    }
}