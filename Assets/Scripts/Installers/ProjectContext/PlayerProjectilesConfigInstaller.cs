using Configs;
using UnityEngine;
using Zenject;

public class PlayerProjectilesConfigInstaller : MonoInstaller
{
    [SerializeField] private PlayerProjectilesConfig playerProjectilesConfig;

    public override void InstallBindings()
    {
        Container.Bind<PlayerProjectilesConfig>().FromInstance(playerProjectilesConfig).AsSingle();
    }
}
