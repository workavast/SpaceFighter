using System.Collections.Generic;
using Projectiles.Enemy;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnemyProjectilesPrefabsConfig", menuName = "SO/EnemyProjectilesPrefabsConfig")]
    public class EnemyProjectilesPrefabsConfig : ScriptableObject
    {
        [SerializeField] private DictionaryInspector<EnemyProjectileType, GameObject> data;
        
        public IReadOnlyDictionary<EnemyProjectileType, GameObject> Data => data;
    }
}