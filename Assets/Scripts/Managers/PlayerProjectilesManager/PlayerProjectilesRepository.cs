using System;
using System.Collections.Generic;
using Factories;
using Projectiles.Player;
using UnityEngine;

namespace Managers
{
    public class PlayerProjectilesRepository
    {
        private readonly List<PlayerProjectileBase> _projectiles = new();
        private readonly PlayerProjectilesFactory _factory;

        public IReadOnlyList<PlayerProjectileBase> Projectiles => _projectiles;

        public event Action<PlayerProjectileBase> OnAdd;
        public event Action<PlayerProjectileBase> OnRemove;
        
        public PlayerProjectilesRepository(PlayerProjectilesFactory factory)
        {
            _factory = factory;
            _factory.OnCreate += Add;
        }
        
        private void Add(PlayerProjectileBase newProjectile)
        {
            if (_projectiles.Contains(newProjectile))
            {
                Debug.LogWarning($"Duplicate of {newProjectile.PoolId}");
                return;
            }
            
            _projectiles.Add(newProjectile);
            newProjectile.ReturnElementEvent += Remove;
            OnAdd?.Invoke(newProjectile);
        }

        private void Remove(PlayerProjectileBase projectile)
        {
            if (!_projectiles.Remove(projectile))
                Debug.LogWarning($"Repository dont contain {projectile.PoolId}");
            else
            {
                projectile.ReturnElementEvent -= Remove;
                OnRemove?.Invoke(projectile);
            }
        }
    }
}