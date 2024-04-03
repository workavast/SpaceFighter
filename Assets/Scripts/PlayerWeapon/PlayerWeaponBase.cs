using System;
using System.Collections.Generic;
using Configs;
using Factories;
using Projectiles.Player;
using Saves;
using SomeStorages;
using UnityEngine;
using Zenject;

namespace PlayerWeapon
{
    public abstract class PlayerWeaponBase : MonoBehaviour
    {
        public abstract PlayerWeaponType PlayerWeaponId { get; }
        public abstract PlayerProjectileType ProjectileId { get; }
    
        [Inject] protected PlayerWeaponConfig PlayerWeaponConfig;
        [Inject] protected PlayerProjectilesFactory PlayerProjectilesFactory;
    
        [SerializeField] protected List<Transform> ShootsPositions;

        protected float Damage;
        protected SomeStorageFloat FireRate;
        protected int ShootsCountScale;
        protected bool CanShoot = true;

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
