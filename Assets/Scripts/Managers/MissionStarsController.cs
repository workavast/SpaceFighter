using Controllers;
using EventBus.Events;
using EventBusExtension;
using UnityEngine;

namespace Managers
{
    public class MissionStarsController : IEventReceiver<PlayerTakeDamage>
    {
        public ReceiverIdentifier ReceiverIdentifier { get; } = new();

        private readonly EventBusExtension.EventBus  _eventBus;
        private readonly MissionController _missionController;
        private int _missionIndex { get; }

        public int StarsCount { get; private set; } = 3;
        public bool PlayerTakeDamage { get; private set; } = false;
        public bool MissionSuccess { get; private set; } = false;
        public bool KillAllEnemies { get; private set; } = false;
        
        public MissionStarsController(EventBusExtension.EventBus eventBus, MissionController missionController, int missionIndex)
        {
            Debug.LogWarning($"mission index for setting {_missionIndex}");
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
            Debug.LogWarning($"mission index for saving pgd inv {_missionIndex}");
            PlayerGlobalData.ChangeMissionData(_missionIndex, StarsCount);
        }
    }
}
