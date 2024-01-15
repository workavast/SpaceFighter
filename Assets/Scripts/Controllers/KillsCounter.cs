using System;
using EventBus.Events;
using EventBusExtension;
using SomeStorages;

namespace Controllers
{
    public class KillsCounter : IEventReceiver<EnemyStartDie>, IDisposable
    {
        public ReceiverIdentifier ReceiverIdentifier { get; } = new();

        private readonly EventBusExtension.EventBus _eventBus;
        private readonly SomeStorageInt _destroyedEnemiesCounter;
        
        private bool _disposed;

        public IReadOnlySomeStorage<int> DestroyedEnemiesCounter => _destroyedEnemiesCounter;

        public KillsCounter(int enemiesCount, EventBusExtension.EventBus missionEventBus)
        {
            _eventBus = missionEventBus;
            _destroyedEnemiesCounter = new SomeStorageInt(enemiesCount);
            _eventBus.Subscribe<EnemyStartDie>(this);
        }

        public void OnEvent(EnemyStartDie @event) => _destroyedEnemiesCounter.ChangeCurrentValue(1);
        
        public void Dispose()
        {
            if(_disposed) return;

            _eventBus.UnSubscribe<EnemyStartDie>(this);
            _disposed = true;
        }
    }
}