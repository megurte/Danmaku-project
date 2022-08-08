using System.Collections.Generic;
using UnityEngine;

namespace Spawner
{
    [CreateAssetMenu(fileName = "new SIConfig", menuName = "SIConfig")]
    public class SpawnerIteratorConfig : ScriptableObject
    {
        public List<EnemyWave> enemyWaves;
    }
}