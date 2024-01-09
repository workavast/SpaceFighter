using Managers;
using Zenject;

namespace Installers
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