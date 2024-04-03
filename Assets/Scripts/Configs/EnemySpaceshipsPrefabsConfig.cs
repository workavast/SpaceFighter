using System.Collections.Generic;
using SpaceShips.Enemies;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnemySpaceshipsPrefabsConfig", menuName = "SO/EnemySpaceshipsPrefabsConfig")]
    public class EnemySpaceshipsPrefabsConfig : ScriptableObject
    {
        [SerializeField] private DictionaryInspector<EnemySpaceshipType, GameObject> data;
       
        public IReadOnlyDictionary<EnemySpaceshipType, GameObject> Data => data;
    }
}
