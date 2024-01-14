using System;
using System.Collections.Generic;
using Configs;
using Projectiles.Enemy;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class EnemyProjectilesFactory : FactoryBase
    {
        [Inject] private EnemyProjectilesPrefabsConfig _enemyPrefabsConfig;
        [Inject] private DiContainer _diContainer;

        private IReadOnlyDictionary<EnemyProjectilesEnum, GameObject> SpaceShipsData => _enemyPrefabsConfig.Data;
    
        public GameObject Create(EnemyProjectilesEnum id)
        {
            if (SpaceShipsData.TryGetValue(id, out GameObject prefab)) 
                return _diContainer.InstantiatePrefab(prefab);
        
            throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
        }
    
        public GameObject Create(EnemyProjectilesEnum id, Transform parent)
        {
            if (SpaceShipsData.TryGetValue(id, out GameObject prefab)) 
                return _diContainer.InstantiatePrefab(prefab, parent);
        
            throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
        }
        
        public GameObject Create(EnemyProjectilesEnum id, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (SpaceShipsData.TryGetValue(id, out GameObject prefab)) 
                return _diContainer.InstantiatePrefab(prefab, position, rotation, parent);
        
            throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
        }
    }
}
