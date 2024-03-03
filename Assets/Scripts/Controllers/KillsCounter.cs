using EventBusEvents;
using EventBusExtension;
using SomeStorages;

namespace Controllers
{
    public class KillsCounter : Disposable, IEventReceiver<EnemyStartDie>
    {
        public EventBusReceiverIdentifier EventBusReceiverIdentifier { get; } = new();

        private readonly EventBus _eventBus;
        private readonly SomeStorageInt _destroyedEnemiesCounter;
        
        public IReadOnlySomeStorage<int> DestroyedEnemiesCounter => _destroyedEnemiesCounter;

        public KillsCounter(int enemiesCount, EventBus missionEventBus)
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