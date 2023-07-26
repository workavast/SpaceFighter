using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyProjectilesPrefabsConfig", menuName = "SO/Factories/EnemyProjectilesPrefabsConfig")]
public class EnemyProjectilesPrefabsConfig : ScriptableObject
{
    [SerializeField] private DictionaryInspector<EnemyProjectilesEnum, GameObject> data;
    public IReadOnlyDictionary<EnemyProjectilesEnum, GameObject> Data => data;
}