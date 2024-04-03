using System;
using System.Collections.Generic;
using Factories;
using SpaceShips.Enemies;
using UnityEngine;

namespace Managers
{
    public class EnemySpaceshipsRepository
    {
        private readonly List<EnemySpaceshipBase> _enemies = new();
        private readonly EnemySpaceshipsFactory _factory;

        public IReadOnlyList<EnemySpaceshipBase> Enemies => _enemies;

        public event Action<EnemySpaceshipBase> OnAdd;
        public event Action<EnemySpaceshipBase> OnRemove;
        
        public EnemySpaceshipsRepository(EnemySpaceshipsFactory factory)
        {
            _factory = factory;
            _factory.OnCreate += Add;
        }
        
        private void Add(EnemySpaceshipBase newEnemySpaceship)
        {
            if (_enemies.Contains(newEnemySpaceship))
            {
                Debug.LogWarning($"Duplicate of {newEnemySpaceship.PoolId}");
                return;
            }
            
            _enemies.Add(newEnemySpaceship);
            newEnemySpaceship.ReturnElementEvent += Remove;
            
            OnAdd?.Invoke(newEnemySpaceship);
        }

        private void Remove(EnemySpaceshipBase enemySpaceship)
        {
            if (!_enemies.Remove(enemySpaceship))
                Debug.LogWarning($"Repository dont contain {enemySpaceship.PoolId}");
            else
            {
                enemySpaceship.ReturnElementEvent -= Remove;
                
                OnRemove?.Invoke(enemySpaceship);
            }
        }
    }
}