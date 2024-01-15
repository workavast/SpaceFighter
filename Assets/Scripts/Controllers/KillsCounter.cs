using DefaultNamespace;
using EventBus.Events;
using EventBusExtension;
using SomeStorages;

namespace Controllers
{
    public class KillsCounter : Disposable, IEventReceiver<EnemyStartDie>
    {
        public ReceiverIdentifier ReceiverIdentifier { get; } = new();

        private readonly EventBusExtension.EventBus _eventBus;
        private readonly SomeStorageInt _destroyedEnemiesCounter;
        
        public IReadOnlySomeStorage<int> DestroyedEnemiesCounter => _destroyedEnemiesCounter;

        public KillsCounter(int enemiesCount, EventBusExtension.EventBus missionEventBus)
        {
            _eventBus = missionEventBus;
            _destroyedEnemiesCounter = new SomeStorageInt(enemiesCount);
            _eventBus.Subscribe<EnemyStartDie>(this);
        }

        public void OnEvent(EnemyStartDie @event) => _destroyedEnemiesCounter.ChangeCurrentValue(1);
        
        protected override void OnDispose()
        {
            _eventBus?.UnSubscribe<EnemyStartDie>(this);
        }
    }
}