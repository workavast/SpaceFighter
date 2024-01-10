using Configs;
using UnityEngine;
using Zenject;

public class PlayerWeaponConfigInstaller : MonoInstaller
{
    [SerializeField] private PlayerWeaponConfig playerWeaponConfig;

    public override void InstallBindings()
    {
        Container.Bind<PlayerWeaponConfig>().FromInstance(playerWeaponConfig).AsSingle();
    }
}
