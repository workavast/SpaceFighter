using System;
using System.Collections.Generic;
using Configs;
using EnumValuesLibrary;
using PoolSystem;
using SpaceShips.Enemies;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class EnemySpaceshipsFactory : FactoryBase
    {
        [Inject] private readonly EnemySpaceshipsPrefabsConfig _enemySpaceshipsPrefabsConfig;
        [Inject] private readonly DiContainer _diContainer;
        
        private readonly Dictionary<EnemySpaceshipType, GameObject> _parents = new();
        private Pool<EnemySpaceshipBase, EnemySpaceshipType> _pool;

        private IReadOnlyDictionary<EnemySpaceshipType, GameObject> EnemySpaceshipsPrefabsData => _enemySpaceshipsPrefabsConfig.Data;
        
        public event Action<EnemySpaceshipBase> OnCreate;

        private void Awake()
        {
            _pool = new Pool<EnemySpaceshipBase, EnemySpaceshipType>(EnemyProjectileInstantiate);

            foreach (var enemyShipId in EnumValuesTool.GetValues<EnemySpaceshipType>())
            {
                GameObject parent = new GameObject(enemyShipId.ToString()) { transform = { parent = transform } };
                _parents.Add(enemyShipId, parent);
            }
        }
        
        public EnemySpaceshipBase Create(EnemySpaceshipType id)
        {
            if(!_pool.ExtractElement(id, out var projectile))
                throw new Exception($"EnemySpaceshipType {id} wasn't extract from pool");
            
            OnCreate?.Invoke(projectile);
            
            return projectile;
        }
        
        private EnemySpaceshipBase EnemyProjectileInstantiate(EnemySpaceshipType id)
        {
            if (!EnemySpaceshipsPrefabsData.TryGetValue(id, out GameObject prefab))
                throw new Exception($"EnemyProjectileType: {id}, dont present in config {_enemySpaceshipsPrefabsConfig}");

            var enemySpaceship = _diContainer.InstantiatePrefab(prefab, _parents[id].transform)
                .GetComponent<EnemySpaceshipBase>();

            return enemySpaceship;
        }
    }
}