using Managers;
using UnityEngine;
using Zenject;

namespace Installers.SceneContext
{
    public class PLayerSpaceshipManagerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerSpaceshipManager playerSpaceshipManager;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerSpaceshipManager>().FromInstance(playerSpaceshipManager).AsSingle();
        }
    }
}