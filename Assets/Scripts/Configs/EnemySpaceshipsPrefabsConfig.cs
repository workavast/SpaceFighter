using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpaceshipsPrefabsConfig", menuName = "SO/Factories/EnemySpaceshipsPrefabsConfig")]
public class EnemySpaceshipsPrefabsConfig : ScriptableObject
{
    [SerializeField] private DictionaryInspector<EnemySpaceshipsEnum, GameObject> data;
    public IReadOnlyDictionary<EnemySpaceshipsEnum, GameObject> Data => data;
}
