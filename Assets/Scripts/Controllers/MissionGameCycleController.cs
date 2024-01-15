using GameCycle;
using Managers;
using UI_System;
using UnityEngine;

namespace Controllers
{
    public class MissionGameCycleController
    {
        private readonly IGameCycleManagerSwitcher _gameCycleManager;
        private readonly UI_Controller _uiController;
        private readonly PlayerSpaceshipManager _playerSpaceshipManager;
        private readonly WavesManager _wavesManager;
        private readonly MoneyStarsManager _moneyStarsManager;

        public MissionGameCycleController(IGameCycleManagerSwitcher gameCycleManager, UI_Controller uiController, 
            PlayerSpaceshipManager playerSpaceshipManager, WavesManager wavesManager, MoneyStarsManager moneyStarsManager)
        {
            _gameCycleManager = gameCycleManager;
            _uiController = uiController;
            _playerSpaceshipManager = playerSpaceshipManager;
            _wavesManager = wavesManager;
            _moneyStarsManager = moneyStarsManager;
            
            _uiController.OnScreenSwitch += OnScreenSwitch;
            _playerSpaceshipManager.OnPlayerDie += OnPlayerDie;
            _wavesManager.OnWavesEnd += OnMissionCompleted;
        }

        private void OnMissionCompleted()
        {
            Debug.Log($"Mission completed");
            _gameCycleManager.SwitchState(GameStatesType.Pause);
            _moneyStarsManager.ApplyMoneyStars();
            _uiController.SetScreen(ScreenType.GameplayMissionEnd);
        }
        
        private void OnPlayerDie()
        {
            _gameCycleManager.SwitchState(GameStatesType.Pause);
            _uiController.SetScreen(ScreenType.GameplayMissionEnd);
        }

        private void OnScreenSwitch(ScreenType screen)
        {
            switch (screen)
            {
                case ScreenType.GameplayMain:
                    OnDeactivatePause();
                    break;
                case ScreenType.GameplayMenu:
                    OnActivatePause();
                    break;
                case ScreenType.GameplayMissionEnd:
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