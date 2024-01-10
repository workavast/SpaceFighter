using System;
using Configs;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class MoneyStarsFactory : FactoryBase
    {
        [Inject] private MoneyStarPrefabConfig _playerProjectilesPrefabConfig;
        private GameObject MoneyStarData => _playerProjectilesPrefabConfig.Data;
    
        public GameObject Create()
        {
            if (MoneyStarData) 
                return Instantiate(MoneyStarData);
        
            throw new Exception("Dictionary don't contain this prefab");
        }
    
        public GameObject Create(Transform parent)
        {
            if (MoneyStarData) 
                return Instantiate(MoneyStarData, parent);
        
            throw new Exception("Dictionary don't contain this prefab");
        }
    
        public GameObject Create(Transform parent, bool worldPositionStay)
        {
            if (MoneyStarData) 
                return Instantiate(MoneyStarData, parent, worldPositionStay);
        
            throw new Exception("Dictionary don't contain this prefab");
        }
    
        public GameObject Create(Vector3 position, Quaternion rotation)
        {
            if (MoneyStarData) 
                return Instantiate(MoneyStarData, position, rotation);
        
            throw new Exception("Dictionary don't contain this prefab");
        }
    
        public GameObject Create(Vector3 position, Quaternion rotation, Transform parent)
        {
            if (MoneyStarData) 
                return Instantiate(MoneyStarData, position, rotation, parent);
        
            throw new Exception("Dictionary don't contain this prefab");
        }
    }
}
