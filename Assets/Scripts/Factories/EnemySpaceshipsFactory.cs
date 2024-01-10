using System;
using System.Collections.Generic;
using Configs;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class EnemySpaceshipsFactory : FactoryBase
    {
        [Inject] private EnemySpaceshipsPrefabsConfig _enemySpaceshipsPrefabsConfig;
        [Inject] private DiContainer _diContainer;

        private IReadOnlyDictionary<EnemySpaceshipsEnum, GameObject> EnemySpaceshipsPrefabsData => _enemySpaceshipsPrefabsConfig.Data;
    
        public GameObject Create(EnemySpaceshipsEnum id)
        {
            if (EnemySpaceshipsPrefabsData.TryGetValue(id, out GameObject prefab))
                return _diContainer.InstantiatePrefab(prefab);
        
            throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
        }
    
        public GameObject Create(EnemySpaceshipsEnum id, Transform parent)
        {
            if (EnemySpaceshipsPrefabsData.TryGetValue(id, out GameObject prefab)) 
                return _diContainer.InstantiatePrefab(prefab, parent);
        
            throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
        }
    
        public GameObject Create(EnemySpaceshipsEnum id, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (EnemySpaceshipsPrefabsData.TryGetValue(id, out GameObject prefab))
                return _diContainer.InstantiatePrefab(prefab, position, rotation, parent);

            throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
        }
    }
}