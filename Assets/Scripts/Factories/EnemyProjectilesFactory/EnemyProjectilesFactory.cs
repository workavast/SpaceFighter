using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyProjectilesFactory : MonoBehaviour
{
    [Inject] private EnemyProjectilesPrefabsConfig _enemyPrefabsConfig;
    private IReadOnlyDictionary<EnemyProjectilesEnum, GameObject> SpaceShipsData => _enemyPrefabsConfig.Data;

    private static EnemyProjectilesFactory _instance;
    
    private void Awake()
    {
        if (_instance)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    public static GameObject Create(EnemyProjectilesEnum id)
    {
        if (_instance.SpaceShipsData.TryGetValue(id, out GameObject prefab)) 
            return Instantiate(prefab);
        
        throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
    }
    
    public static GameObject Create(EnemyProjectilesEnum id, Transform parent)
    {
        if (_instance.SpaceShipsData.TryGetValue(id, out GameObject prefab)) 
            return Instantiate(prefab, parent);
        
        throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
    }
    
    public static GameObject Create(EnemyProjectilesEnum id, Transform parent, bool worldPositionStay)
    {
        if (_instance.SpaceShipsData.TryGetValue(id, out GameObject prefab)) 
            return Instantiate(prefab, parent, worldPositionStay);
        
        throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
    }
    
    public static GameObject Create(EnemyProjectilesEnum id, Vector3 position, Quaternion rotation)
    {
        if (_instance.SpaceShipsData.TryGetValue(id, out GameObject prefab)) 
            return Instantiate(prefab, position, rotation);
        
        throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
    }
    
    public static GameObject Create(EnemyProjectilesEnum id, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (_instance.SpaceShipsData.TryGetValue(id, out GameObject prefab)) 
            return Instantiate(prefab, position, rotation, parent);
        
        throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
    }
}
