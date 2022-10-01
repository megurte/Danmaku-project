using System.Collections.Generic;
using UnityEngine;

namespace Spawner
{
    [CreateAssetMenu(fileName = "new Spawner", menuName = "Spawner")]
    public class SpawnerSO : ScriptableObject
    {
        public List<EnemySpawnProperties> enemySpawnPropertiesList;
    }
}