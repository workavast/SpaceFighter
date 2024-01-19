using EventBus;
using Zenject;

namespace Installers.SceneContext
{
    public class MissionEventBusInstaller : MonoInstaller
    {
        private readonly MissionEventBus _missionEventBus = new();
        
        public override void InstallBindings()
        {
            Container.Bind<MissionEventBus>().FromInstance(_missionEventBus).AsSingle();
        }
    }
}