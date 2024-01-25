using System;
using Configs;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class CoinsFactory : FactoryBase
    {
        [Inject] private CoinPrefabConfig _prefabConfig;
        [Inject] private DiContainer _diContainer;

        private GameObject CoinPrefab => _prefabConfig.Data;
    
        public GameObject Create()
        {
            if (CoinPrefab) 
                return _diContainer.InstantiatePrefab(CoinPrefab);
        
            throw new Exception("Dictionary don't contain this prefab");
        }
    
        public GameObject Create(Transform parent)
        {
            if (CoinPrefab) 
                return _diContainer.InstantiatePrefab(CoinPrefab, parent);
        
            throw new Exception("Dictionary don't contain this prefab");
        }
        
        public GameObject Create(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (CoinPrefab) 
                return _diContainer.InstantiatePrefab(CoinPrefab, position, rotation, parent);
        
            throw new Exception("Dictionary don't contain this prefab");
        }
    }
}
