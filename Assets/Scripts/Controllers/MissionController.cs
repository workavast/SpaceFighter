using EventBusExtension;
using GameCycle;
using Managers;
using UI_System;
using Zenject;

namespace Controllers
{
    public class MissionController : ControllerBase
    {
        [Inject] private IGameCycleSwitcher _gameCycleSwitcher;
        [Inject] private UI_Controller _uiController;
        [Inject] private PlayerSpaceshipManager _playerSpaceshipManager;
        [Inject] private WavesManager _wavesManager;
        [Inject] private SelectedMissionData _selectedMissionData;
        [Inject] private EventBus _eventBus;
        [Inject] private CoinsManager _coinsManager;
        [Inject] private EnemySpaceshipsManager _enemySpaceshipsManager;
        
        public KillsCounter KillsCounter { get; private set; }

        private MissionGameCycleController _missionGameCycleController;
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