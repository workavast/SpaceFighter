using System.Collections.Generic;
using Factories;
using Projectiles.Enemy;
using SomeStorages;
using UnityEngine;
using Zenject;

namespace SpaceShips.Enemies
{
    public abstract class ShootingEnemySpaceshipBase : EnemySpaceshipBase, IPlayAreaCollision
    {
        protected abstract EnemyProjectileType ProjectileId { get; }
    
        [Space]
        [SerializeField] protected List<Transform> shootPositions;
        [SerializeField] protected SomeStorageFloat fireRate;
        [SerializeField] protected bool canShoot;

        [Inject] protected readonly EnemyProjectilesFactory EnemyProjectilesFactory;

        private bool _inPlayArea;
        private bool _canShoot;

        protected override void OnAwake()
        {
            base.OnAwake();
            _canShoot = canShoot;

            OnElementExtractFromPoolEvent += OnElementExtractFromPoolMethod;
            OnHandleUpdate += TryShoot;
        }

        public void EnterInPlayArea()
            => _inPlayArea = true;
        
        public void ExitFromPlayerArea()
            => _inPlayArea = false;
        
        private void TryShoot()
        {
            fireRate.ChangeCurrentValue(Time.deltaTime);
            if (fireRate.IsFull && _inPlayArea && _canShoot)
            {
                Shoot();
                fireRate.SetCurrentValue(0);
            }
        }

        private void Shoot()
        {
            foreach (var shootPos in shootPositions)
                EnemyProjectilesFactory.Create(ProjectileId, shootPos);
        }
    
        private void OnElementExtractFromPoolMethod()
        {
            _canShoot = canShoot;
            fireRate.SetCurrentValue(0);
        }
    }
}
