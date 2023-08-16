using System.Collections.Generic;
using UnityEngine;

namespace MissionsDataConfigsSystem
{
    [CreateAssetMenu(fileName = "EnemyWavesConfig", menuName = "SO/EnemyWavesConfig")]
    public class EnemyWavesConfig : ScriptableObject
    {
        [field: SerializeField] public List<EnemyWaveConfig> enemyWaves { get; private set; }
    }
}
