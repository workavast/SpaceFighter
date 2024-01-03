using System;
using UnityEngine;
using Zenject;

public class MoneyStarsFactory : MonoBehaviour
{
    [Inject] private MoneyStarPrefabConfig _playerProjectilesPrefabConfig;
    private GameObject MoneyStarData => _playerProjectilesPrefabConfig.Data;
    
    private static MoneyStarsFactory _instance { get; set; }
    
    private void Awake()
    {
        if (_instance)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    public static GameObject Create()
    {
        if (_instance.MoneyStarData) 
            return Instantiate(_instance.MoneyStarData);
        
        throw new Exception("Dictionary don't contain this prefab");
    }
    
    public static GameObject Create(Transform parent)
    {
        if (_instance.MoneyStarData) 
            return Instantiate(_instance.MoneyStarData, parent);
        
        throw new Exception("Dictionary don't contain this prefab");
    }
    
    public static GameObject Create(Transform parent, bool worldPositionStay)
    {
        if (_instance.MoneyStarData) 
            return Instantiate(_instance.MoneyStarData, parent, worldPositionStay);
        
        throw new Exception("Dictionary don't contain this prefab");
    }
    
    public static GameObject Create(Vector3 position, Quaternion rotation)
    {
        if (_instance.MoneyStarData) 
            return Instantiate(_instance.MoneyStarData, position, rotation);
        
        throw new Exception("Dictionary don't contain this prefab");
    }
    
    public static GameObject Create(Vector3 position, Quaternion rotation, Transform parent)
    {
        if (_instance.MoneyStarData) 
            return Instantiate(_instance.MoneyStarData, position, rotation, parent);
        
        throw new Exception("Dictionary don't contain this prefab");
    }
}
