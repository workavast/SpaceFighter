using System;
using System.Collections.Generic;
using Configs;
using Projectiles.Player;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class PlayerProjectilesFactory : FactoryBase
    {
        [Inject] private PlayerProjectilesConfig _playerProjectilesConfig;
        [Inject] private DiContainer _diContainer;
        
        private IReadOnlyDictionary<PlayerProjectilesEnum, GameObject> PlayerProjectilesData => _playerProjectilesConfig.Data;
    
        public GameObject Create(PlayerProjectilesEnum id)
        {
            if (PlayerProjectilesData.TryGetValue(id, out GameObject prefab)) 
                return _diContainer.InstantiatePrefab(prefab);
        
            throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
        }
    
        public GameObject Create(PlayerProjectilesEnum id, Transform parent)
        {
            if (PlayerProjectilesData.TryGetValue(id, out GameObject prefab)) 
                return _diContainer.InstantiatePrefab(prefab, parent);
        
            throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
        }
        
        public GameObject Create(PlayerProjectilesEnum id, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (PlayerProjectilesData.TryGetValue(id, out GameObject prefab)) 
                return _diContainer.InstantiatePrefab(prefab, position, rotation, parent);
        
            throw new Exception("Dictionary don't contain this EnemySpaceshipsEnum");
        }
    }
}
