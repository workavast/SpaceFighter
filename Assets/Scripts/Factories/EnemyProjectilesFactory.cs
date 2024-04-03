using System;
using System.Collections.Generic;
using Configs;
using EnumValuesLibrary;
using PoolSystem;
using Projectiles.Enemy;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class EnemyProjectilesFactory : FactoryBase
    {
        [Inject] private EnemyProjectilesPrefabsConfig _enemyPrefabsConfig;
        [Inject] private DiContainer _diContainer;

        private readonly Dictionary<EnemyProjectileType, GameObject> _projectilesParents = new();
        private Pool<EnemyProjectileBase, EnemyProjectileType> _pool;

        private IReadOnlyDictionary<EnemyProjectileType, GameObject> ProjectilesData => _enemyPrefabsConfig.Data;
        
        public event Action<EnemyProjectileBase> OnCreate;
        
        private void Awake()
        {
            _pool = new Pool<EnemyProjectileBase, EnemyProjectileType>(EnemyProjectileInstantiate);

            foreach (var projectileType in EnumValuesTool.GetValues<EnemyProjectileType>())
            {
                GameObject parent = new GameObject(projectileType.ToString()) { transform = { parent = transform } };
                _projectilesParents.Add(projectileType, parent);
            }
        }

        public EnemyProjectileBase Create(EnemyProjectileType id, Transform spawnTransform)
        {
            if(!_pool.ExtractElement(id, out var projectile))
                throw new Exception($"EnemyProjectileType {id} wasn't extract from pool");
            
            projectile.transform.position = spawnTransform.position;
            projectile.transform.rotation = spawnTransform.rotation;
            
            OnCreate?.Invoke(projectile);
            
            return projectile;
        }
        
        private EnemyProjectileBase EnemyProjectileInstantiate(EnemyProjectileType id)
        {
            if (!ProjectilesData.TryGetValue(id, out GameObject prefab))
                throw new Exception($"EnemyProjectileType: {id}, dont present in config {_enemyPrefabsConfig}");

            var projectile = _diContainer.InstantiatePrefab(prefab, _projectilesParents[id].transform)
                .GetComponent<EnemyProjectileBase>();

            return projectile;
        }
    }
}
