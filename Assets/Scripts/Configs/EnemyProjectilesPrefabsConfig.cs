using System.Collections.Generic;
using Projectiles.Enemy;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnemyProjectilesPrefabsConfig", menuName = "SO/Factories/EnemyProjectilesPrefabsConfig")]
    public class EnemyProjectilesPrefabsConfig : ScriptableObject
    {
        [SerializeField] private DictionaryInspector<EnemyProjectilesEnum, GameObject> data;
        public IReadOnlyDictionary<EnemyProjectilesEnum, GameObject> Data => data;
    }
}