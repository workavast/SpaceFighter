using System.Collections.Generic;
using UnityEngine;

namespace MissionsDataConfigsSystem
{
    [CreateAssetMenu(fileName = "EnemyWaveConfig", menuName = "SO/EnemyWaveConfig")]
    public class EnemyWaveConfig : ScriptableObject
    {
        [field: SerializeField] public List<EnemyGroupConfig> enemyWave { get; private set; }
    }
}