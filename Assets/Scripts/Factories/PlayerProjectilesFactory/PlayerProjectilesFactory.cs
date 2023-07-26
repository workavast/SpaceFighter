using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerProjectilesFactory : MonoBehaviour
{
    [Inject] private PlayerProjectilesConfig _playerProjectilesConfig;
    private IReadOnlyDictionary<PlayerProjectilesEnum, GameObject> PlayerProjectilesData => _playerProjectilesConfig.Data;
    
    private static PlayerProjectilesFactory _instance { get; set; }
    
    private void Awake()
    {
        if (_instance)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    public static GameObject Create(PlayerProjectilesEnum id)
    {
        if (_instance.PlayerProjectilesData.TryGetValue(id, out GameObject prefab)) 
            return Instantiate(prefab);
        
        throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
    }
    
    public static GameObject Create(PlayerProjectilesEnum id, Transform parent)
    {
        if (_instance.PlayerProjectilesData.TryGetValue(id, out GameObject prefab)) 
            return Instantiate(prefab, parent);
        
        throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
    }
    
    public static GameObject Create(PlayerProjectilesEnum id, Transform parent, bool worldPositionStay)
    {
        if (_instance.PlayerProjectilesData.TryGetValue(id, out GameObject prefab)) 
            return Instantiate(prefab, parent, worldPositionStay);
        
        throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
    }
    
    public static GameObject Create(PlayerProjectilesEnum id, Vector3 position, Quaternion rotation)
    {
        if (_instance.PlayerProjectilesData.TryGetValue(id, out GameObject prefab)) 
            return Instantiate(prefab, position, rotation);
        
        throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
    }
    
    public static GameObject Create(PlayerProjectilesEnum id, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (_instance.PlayerProjectilesData.TryGetValue(id, out GameObject prefab)) 
            return Instantiate(prefab, position, rotation, parent);
        
        throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
    }
}
