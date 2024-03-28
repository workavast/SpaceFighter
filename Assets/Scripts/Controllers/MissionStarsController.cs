using EventBusEvents;
using EventBusExtension;
using Saves;

namespace Controllers
{
    public class MissionStarsController : IEventReceiver<PlayerTakeDamage>
    {
        public EventBusReceiverIdentifier EventBusReceiverIdentifier { get; } = new();

        private readonly EventBus  _eventBus;
        private readonly MissionController _missionController;
        private int _missionIndex { get; }

        public int StarsCount { get; private set; } = 3;
        public bool PlayerTakeDamage { get; private set; } = false;
        public bool MissionSuccess { get; private set; } = false;
        public bool KillAllEnemies { get; private set; } = false;
        
        public MissionStarsController(EventBus eventBus, MissionController missionController, int missionIndex)
        {
            _eventBus = eventBus;
            _missionController = missionController;
            _missionIndex = missionIndex;
            
            _eventBus.Subscribe(this);
        }

        public void OnEvent(PlayerTakeDamage @event)
        {
            PlayerTakeDamage = true;
            StarsCount -= 1;
            _eventBus.UnSubscribe(this);
        }

        public void OnMissionCompleted()
        {
            MissionSuccess = true;
            KillAllEnemies = _missionController.KillsCounter.DestroyedEnemiesCounter.IsFull;
            if (!KillAllEnemies) StarsCount -= 1;

            ApplyStars();
        }

        public void OnMissionLoosed()
        {
            OnEvent(new PlayerTakeDamage());
            MissionSuccess = false;
            KillAllEnemies = false;
            StarsCount = 0;
        }
        
        private void ApplyStars()
        {
            PlayerGlobalData.Instance.MissionsSettings.ChangeMissionData(_missionIndex, StarsCount);
        }
    }
}
