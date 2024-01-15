using Controllers;
using EventBus.Events;
using EventBusExtension;

namespace Managers
{
    public class MissionStarsController : IEventReceiver<PlayerTakeDamage>
    {
        public ReceiverIdentifier ReceiverIdentifier { get; } = new();

        private readonly EventBusExtension.EventBus  _eventBus;
        private readonly MissionController _missionController;
        
        public int StarsCount { get; private set; } = 3;
        public bool PlayerTakeDamage { get; private set; } = false;
        public bool MissionSuccess { get; private set; } = false;
        public bool KillAllEnemies { get; private set; } = false;
        
        public MissionStarsController(EventBusExtension.EventBus eventBus, MissionController missionController)
        {
            _eventBus = eventBus;
            _missionController = missionController;
            
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
            PlayerGlobalData.ChangeMissionData(0, StarsCount);
        }
    }
}
