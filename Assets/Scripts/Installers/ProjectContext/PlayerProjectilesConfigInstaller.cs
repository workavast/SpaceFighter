using Configs;
using UnityEngine;
using Zenject;

namespace Installers.ProjectContext
{
    public class PlayerProjectilesConfigInstaller : MonoInstaller
    {
        [SerializeField] private PlayerProjectilesConfig playerProjectilesConfig;

        public override void InstallBindings()
        {
            Container.Bind<PlayerProjectilesConfig>().FromInstance(playerProjectilesConfig).AsSingle();
        }
    }
}
