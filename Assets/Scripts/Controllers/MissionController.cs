using GameCycle;
using Managers;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class MissionController : ControllerBase
    {
        [Inject] private UI_Controller _uiController;
        [Inject] private PlayerSpaceshipManager _playerSpaceshipManager;
        [Inject] private WavesController _wavesController;
        [Inject] private IGameCycleManagerSwitcher _gameCycleManager;
        
        private void Awake()
        {
            _uiController.OnScreenSwitch += OnScreenSwitch;
            _playerSpaceshipManager.OnPlayerDie += OnPlayerDie;
            _wavesController.OnWavesEnd += OnMissionCompleted;
        }

        private void Start()
        {
            _wavesController.StartWaves();
        }

        private void OnMissionCompleted()
        {
            Debug.Log($"Mission completed");
            _gameCycleManager.SwitchState(GameStatesType.Pause);
            LevelMoneyStarsCounter.ApplyValue();
            _uiController.SetScreen(ScreensEnum.GameplayMissionEnd);
        }
        
        private void OnPlayerDie()
        {
            _gameCycleManager.SwitchState(GameStatesType.Pause);
            _uiController.SetScreen(ScreensEnum.GameplayMissionEnd);
        }

        private void OnScreenSwitch(ScreensEnum screen)
        {
            switch (screen)
            {
                case ScreensEnum.GameplayMain:
                    OnDeactivatePause();
                    break;
                case ScreensEnum.GameplayMenu:
                    OnActivatePause();
                    break;
                case ScreensEnum.GameplayMissionEnd:
                    break;
                default:
                    return;
            }
        }
        
        private void OnActivatePause()
        {
            _gameCycleManager.SwitchState(GameStatesType.Pause);
        }

        private void OnDeactivatePause()
        {
            if (_playerSpaceshipManager.PlayerIsDead)
            {
                Debug.LogWarning($"Player is dead");
                return;
            }
            
            _gameCycleManager.SwitchState(GameStatesType.Gameplay);
        }
    }
}