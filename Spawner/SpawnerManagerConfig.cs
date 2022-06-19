using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new SMConfig", menuName = "SMConfig")]
public class SpawnerManagerConfig : ScriptableObject
{
    public List<EnemyWave> EnemyWaves;
}

[Serializable]
public struct EnemyWave
{
    public float waveTime;
    public int spawnerIndex;
}