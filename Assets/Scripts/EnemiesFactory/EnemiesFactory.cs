using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemiesFactory : MonoBehaviour
{
    [Inject] private EnemiesSpaceshipsPrefabsConfig enemiesSpaceshipsPrefabsConfig;
    private IReadOnlyDictionary<EnemySpaceshipsEnum, GameObject> SpaceShipsData => enemiesSpaceshipsPrefabsConfig.SpaceShipsData;

    private static EnemiesFactory _instance;
    
    private void Awake()
    {
        if (_instance)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    public static EnemySpaceShip CreateEnemySpaceship(EnemySpaceshipsEnum id)
    {
        if (_instance.SpaceShipsData.TryGetValue(id, out GameObject prefab)) 
            return Instantiate(prefab).GetComponentInChildren<EnemySpaceShip>();
        
        throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
    }
    
    public static EnemySpaceShip CreateEnemySpaceship(EnemySpaceshipsEnum id, Transform parent)
    {
        if (_instance.SpaceShipsData.TryGetValue(id, out GameObject prefab)) 
            return Instantiate(prefab, parent).GetComponentInChildren<EnemySpaceShip>();
        
        throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
    }
    
    public static EnemySpaceShip CreateEnemySpaceship(EnemySpaceshipsEnum id, Transform parent, bool worldPositionStay)
    {
        if (_instance.SpaceShipsData.TryGetValue(id, out GameObject prefab)) 
            return Instantiate(prefab, parent, worldPositionStay).GetComponentInChildren<EnemySpaceShip>();
        
        throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
    }
    
    public static EnemySpaceShip CreateEnemySpaceship(EnemySpaceshipsEnum id, Vector3 position, Quaternion rotation)
    {
        if (_instance.SpaceShipsData.TryGetValue(id, out GameObject prefab)) 
            return Instantiate(prefab, position, rotation).GetComponentInChildren<EnemySpaceShip>();
        
        throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
    }
    
    public static EnemySpaceShip CreateEnemySpaceship(EnemySpaceshipsEnum id, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (_instance.SpaceShipsData.TryGetValue(id, out GameObject prefab)) 
            return Instantiate(prefab, position, rotation, parent).GetComponentInChildren<EnemySpaceShip>();
        
        throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
    }
}