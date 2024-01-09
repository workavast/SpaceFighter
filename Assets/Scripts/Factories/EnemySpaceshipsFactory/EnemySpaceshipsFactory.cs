using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpaceshipsFactory : MonoBehaviour
{
    [Inject] private EnemySpaceshipsPrefabsConfig _enemySpaceshipsPrefabsConfig;
    [Inject] private DiContainer _diContainer;

    private IReadOnlyDictionary<EnemySpaceshipsEnum, GameObject> EnemySpaceshipsPrefabsData => _enemySpaceshipsPrefabsConfig.Data;
    
    private static EnemySpaceshipsFactory _instance { get; set; }
    
    private void Awake()
    {
        if (_instance)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    public static GameObject Create(EnemySpaceshipsEnum id)
    {
        if (_instance.EnemySpaceshipsPrefabsData.TryGetValue(id, out GameObject prefab))
            return _instance._diContainer.InstantiatePrefab(prefab);
        
        throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
    }
    
    public static GameObject Create(EnemySpaceshipsEnum id, Transform parent)
    {
        if (_instance.EnemySpaceshipsPrefabsData.TryGetValue(id, out GameObject prefab)) 
            return _instance._diContainer.InstantiatePrefab(prefab, parent);
        
        throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
    }
    
    public static GameObject Create(EnemySpaceshipsEnum id, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (_instance.EnemySpaceshipsPrefabsData.TryGetValue(id, out GameObject prefab))
            return _instance._diContainer.InstantiatePrefab(prefab, position, rotation, parent);

        throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
    }
}