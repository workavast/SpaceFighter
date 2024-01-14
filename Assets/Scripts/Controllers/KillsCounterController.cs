using EventBus;
using EventBus.Events;
using EventBusExtension;
using MissionsDataConfigsSystem;
using SomeStorages;
using Zenject;

namespace Controllers
{
    public class KillsCounterController : ControllerBase, IEventReceiver<EnemyStartDie>
    {
        public ReceiverIdentifier ReceiverIdentifier { get; } = new();

        private SomeStorageInt _destroyedEnemiesCounter;
        
        [Inject] private MissionEventBus _eventBus;
        [Inject] private SelectedMissionData _selectedMissionData;

        public IReadOnlySomeStorage<int> DestroyedEnemiesCounter => _destroyedEnemiesCounter;

        private void Awake()
        {
            _destroyedEnemiesCounter = new SomeStorageInt(_selectedMissionData.TakeMissionData().TakeEnemiesCount());
            _eventBus.Subscribe<EnemyStartDie>(this);
        }

        public void OnEvent(EnemyStartDie ev) => _destroyedEnemiesCounter.ChangeCurrentValue(1);
    }
}