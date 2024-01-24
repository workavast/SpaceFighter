using EventBusExtension;
using Zenject;

namespace Installers.SceneContext
{
    public class EventBusInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var eventBas = new EventBus();
            
            Container.Bind<EventBus>().FromInstance(eventBas).AsSingle();
        }
    }
}