using UnityEngine;
using Zenject;

public class PlayerSpaceshipLevelsConfigInstaller : MonoInstaller
{
    [SerializeField] private PlayerSpaceshipLevelsConfig playerSpaceshipLevelsConfig;

    public override void InstallBindings()
    {
        Container.Bind<PlayerSpaceshipLevelsConfig>().FromInstance(playerSpaceshipLevelsConfig).AsSingle();
    }
}
