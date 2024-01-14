using System;
using Configs;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class MoneyStarsFactory : FactoryBase
    {
        [Inject] private MoneyStarPrefabConfig _playerProjectilesPrefabConfig;
        [Inject] private DiContainer _diContainer;

        private GameObject MoneyStarPrefab => _playerProjectilesPrefabConfig.Data;
    
        public GameObject Create()
        {
            if (MoneyStarPrefab) 
                return _diContainer.InstantiatePrefab(MoneyStarPrefab);
        
            throw new Exception("Dictionary don't contain this prefab");
        }
    
        public GameObject Create(Transform parent)
        {
            if (MoneyStarPrefab) 
                return _diContainer.InstantiatePrefab(MoneyStarPrefab, parent);
        
            throw new Exception("Dictionary don't contain this prefab");
        }
        
        public GameObject Create(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (MoneyStarPrefab) 
                return _diContainer.InstantiatePrefab(MoneyStarPrefab, position, rotation, parent);
        
            throw new Exception("Dictionary don't contain this prefab");
        }
    }
}
