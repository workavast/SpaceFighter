using EventBusExtension;
using GameCycle;
using Managers;
using UI_System;
using Zenject;

namespace Controllers
{
    public class MissionController : ControllerBase
    {
        [Inject] private readonly IGameCycleSwitcher _gameCycleSwitcher;
        [Inject] private readonly PlayerSpaceshipManager _playerSpaceshipManager;
        [Inject] private readonly EnemySpaceshipsManager _enemySpaceshipsManager;
        [Inject] private readonly SelectedMissionData _selectedMissionData;
        [Inject] private readonly UI_Controller _uiController;
        [Inject] private readonly WavesManager _wavesManager;
        [Inject] private readonly CoinsManager _coinsManager;
        [Inject] private readonly EventBus _eventBus;
        
        private MissionGameCycleController _missionGameCycleController;
        
        public KillsCounter KillsCounter { get; private set; }
        public MissionStarsController StarsController { get; private set; }
        
        private void Awake()
        {
            StarsController = new MissionStarsController(_eventBus, this, _selectedMissionData.TakeMissionIndex());
            KillsCounter = new KillsCounter(_selectedMissionData.TakeMissionData().TakeEnemiesCount(), _eventBus);

            _missionGameCycleController = new MissionGameCycleController(_gameCycleSwitcher, _uiController,
                _playerSpaceshipManager, _wavesManager, _coinsManager, _enemySpaceshipsManager, StarsController);
        }
        
        private void Start()
        {
            _wavesManager.StartWaves();
        }

        private void OnDestroy()
        {
            _missionGameCycleController.Dispose();
            KillsCounter.Dispose();
        }
    }
}