using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesSpaceshipConfig", menuName = "ScriptableObject/EnemiesSpaceshipsPrefabsConfig")]
public class EnemiesSpaceshipsPrefabsConfig : ScriptableObject
{
    [SerializeField] private DictionaryInspector<EnemySpaceshipsEnum, GameObject> spaceShipsData;
    public IReadOnlyDictionary<EnemySpaceshipsEnum, GameObject> SpaceShipsData => spaceShipsData;
}
