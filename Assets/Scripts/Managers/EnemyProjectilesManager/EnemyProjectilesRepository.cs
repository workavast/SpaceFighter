using System.Collections.Generic;
using Factories;
using Projectiles.Enemy;
using UnityEngine;

namespace Managers
{
    public class EnemyProjectilesRepository
    {
        private readonly List<EnemyProjectileBase> _projectiles = new();
        private readonly EnemyProjectilesFactory _factory;

        public IReadOnlyList<EnemyProjectileBase> Projectiles => _projectiles;
        
        public EnemyProjectilesRepository(EnemyProjectilesFactory factory)
        {
            _factory = factory;
            _factory.OnCreate += Add;
        }
        
        private void Add(EnemyProjectileBase newProjectile)
        {
            if (_projectiles.Contains(newProjectile))
            {
                Debug.LogWarning($"Duplicate of {newProjectile.PoolId}");
                return;
            }
            
            _projectiles.Add(newProjectile);
            newProjectile.ReturnElementEvent += Remove;
        }

        private void Remove(EnemyProjectileBase projectile)
        {
            if (!_projectiles.Remove(projectile))
                Debug.LogWarning($"Repository dont contain {projectile.PoolId}");
            else
                projectile.ReturnElementEvent -= Remove;
        }
    }
}