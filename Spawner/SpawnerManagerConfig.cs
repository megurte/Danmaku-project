using System.Collections.Generic;
using UnityEngine;

namespace Spawner
{
    [CreateAssetMenu(fileName = "new SMConfig", menuName = "SMConfig")]
    public class SpawnerManagerConfig : ScriptableObject
    {
        public List<EnemyWave> enemyWaves;
    }
}