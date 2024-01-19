using Configs;
using UnityEngine;
using Zenject;

namespace Installers.ProjectContext
{
    public class PlayerSpaceshipLevelsConfigInstaller : MonoInstaller
    {
        [SerializeField] private PlayerSpaceshipLevelsConfig playerSpaceshipLevelsConfig;

        public override void InstallBindings()
        {
            Container.Bind<PlayerSpaceshipLevelsConfig>().FromInstance(playerSpaceshipLevelsConfig).AsSingle();
        }
    }
}
