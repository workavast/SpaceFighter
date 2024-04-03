using System;
using System.Collections.Generic;
using Configs;
using EnumValuesLibrary;
using PoolSystem;
using Projectiles.Player;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class PlayerProjectilesFactory : FactoryBase
    {
        [Inject] private PlayerProjectilesConfig _playerProjectilesConfig;
        [Inject] private DiContainer _diContainer;
        
        private readonly Dictionary<PlayerProjectileType, GameObject> _projectilesParents = new();
        private Pool<PlayerProjectileBase, PlayerProjectileType> _pool;

        private IReadOnlyDictionary<PlayerProjectileType, GameObject> PlayerProjectilesData => _playerProjectilesConfig.Data;
    
        public event Action<PlayerProjectileBase> OnCreate;
        
        private void Awake()
        {
            _pool = new Pool<PlayerProjectileBase, PlayerProjectileType>(ProjectileInstantiate);

            foreach (var projectileType in EnumValuesTool.GetValues<PlayerProjectileType>())
            {
                GameObject parent = new GameObject(projectileType.ToString()) { transform = { parent = transform } };
                _projectilesParents.Add(projectileType, parent);
            }
        }
        
        public PlayerProjectileBase Create(PlayerProjectileType id, Transform spawnTransform)
        {
            if(!_pool.ExtractElement(id, out var projectile))
                throw new Exception($"EnemyProjectileType {id} wasn't extract from pool");
            
            projectile.transform.position = spawnTransform.position;
            projectile.transform.rotation = spawnTransform.rotation;
            
            OnCreate?.Invoke(projectile);
            
            return projectile;
        }

        private PlayerProjectileBase ProjectileInstantiate(PlayerProjectileType id)
        {
            if (!PlayerProjectilesData.TryGetValue(id, out GameObject prefab))
                throw new Exception($"Projectile ({id}), dont present in config {_playerProjectilesConfig}");

            var projectile = _diContainer.InstantiatePrefab(prefab, _projectilesParents[id].transform)
                .GetComponent<PlayerProjectileBase>();

            return projectile;
        }
    }
}
