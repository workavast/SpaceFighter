using EventBus;
using GameCycle;
using Managers;
using MissionsDataConfigsSystem;
using UI_System;
using Zenject;

namespace Controllers
{
    public class MissionController : ControllerBase
    {
        [Inject] private IGameCycleManagerSwitcher _gameCycleManager;
        [Inject] private UI_Controller _uiController;
        [Inject] private PlayerSpaceshipManager _playerSpaceshipManager;
        [Inject] private WavesManager _wavesManager;
        [Inject] private SelectedMissionData _selectedMissionData;
        [Inject] private MissionEventBus _missionEventBus;
        [Inject] private MoneyStarsManager _moneyStarsManager;
        
        public KillsCounter KillsCounter { get; private set; }

        private MissionGameCycleController _missionGameCycleController;
        
        private void Awake()
        {
            _missionGameCycleController = new MissionGameCycleController(_gameCycleManager, _uiController,
                _playerSpaceshipManager, _wavesManager, _moneyStarsManager);
            
            KillsCounter = new KillsCounter(_selectedMissionData.TakeMissionData().TakeEnemiesCount(), _missionEventBus.EventBus);
        }

        private void Start()
        {
            _wavesManager.StartWaves();
        }

        private void OnDestroy()
        {
            KillsCounter.Dispose();
        }
    }
}