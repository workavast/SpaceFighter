using System;
using System.Collections.Generic;
using Configs;
using Factories;
using GameCycle;
using Projectiles.Player;
using Saves;
using SomeStorages;
using UnityEngine;
using Zenject;

namespace PlayerWeapon
{
    public abstract class PlayerWeaponBase : MonoBehaviour
    {
        [SerializeField] protected List<Transform> ShootsPositions;

        [Inject] protected readonly PlayerWeaponConfig PlayerWeaponConfig;
        [Inject] protected readonly PlayerProjectilesFactory PlayerProjectilesFactory;
        
        protected SomeStorageFloat FireRate;
        protected int ShootsCountScale;
        protected bool CanShoot = true;
        protected float Damage;

        public abstract PlayerWeaponType PlayerWeaponId { get; }
        public abstract PlayerProjectileType ProjectileId { get; }
        
        public void Initialization()
        {
            if (!PlayerWeaponConfig.WeaponsLevelsData.ContainsKey(PlayerWeaponId)) throw new Exception("Undefined WeaponsEnum WeaponsId");

            WeaponLevel weaponLevel =
                PlayerWeaponConfig.WeaponsLevelsData[PlayerWeaponId]
                    [PlayerGlobalData.Instance.WeaponsSettings.CurrentWeaponsLevels[PlayerWeaponId] - 1];

            Damage = weaponLevel.WeaponDamage;
            FireRate = new SomeStorageFloat(weaponLevel.FireRate);
            ShootsCountScale = weaponLevel.ShootsCountScale;
        }

        public void HandleUpdate()
        {
            FireRate.ChangeCurrentValue(Time.deltaTime);

            if (CanShoot && FireRate.IsFull)
            {
                Shoot();
                FireRate.SetCurrentValue(0);
            }
        }

        protected virtual void Shoot()
        {
            for (int i = 0; i < ShootsPositions.Count; i++)
            {
                var projectile = PlayerProjectilesFactory.Create(ProjectileId, ShootsPositions[i]);
                projectile.SetData(Damage);
            }
        }

        public void StopShoot()
        {
            CanShoot = false;
        }
    }
}
