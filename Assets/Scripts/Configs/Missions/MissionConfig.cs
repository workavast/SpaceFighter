using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MissionsDataConfigsSystem
{
    [CreateAssetMenu(fileName = "MissionConfig", menuName = "SO/MissionConfig")]
    public class MissionConfig : ScriptableObject
    {
        [field: SerializeField] public List<EnemyWaveConfig> enemyWaves { get; private set; }

        public int TakeEnemiesCount() => enemyWaves.Sum(wave => wave.TakeEnemiesCount());
    }
}
