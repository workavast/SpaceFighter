using DefaultNamespace;
using GameCycle;
using Managers;
using UI_System;
using UnityEngine;

namespace Controllers
{
    public class MissionGameCycleController : Disposable
    {
        private readonly IGameCycleSwitcher _gameCycleSwitcher;
        private readonly UI_Controller _uiController;
        private readonly PlayerSpaceshipManager _playerSpaceshipManager;
        private readonly WavesManager _wavesManager;
        private readonly MoneyStarsManager _moneyStarsManager;
        private readonly EnemySpaceshipsManager _enemySpaceshipsManager;
        private readonly MissionStarsController _missionStarsController;
        
        public MissionGameCycleController(IGameCycleSwitcher gameCycleSwitcher, UI_Controller uiController,
            PlayerSpaceshipManager playerSpaceshipManager, WavesManager wavesManager,
            MoneyStarsManager moneyStarsManager, EnemySpaceshipsManager enemySpaceshipsManager, MissionStarsController missionStarsController)
        {
            _gameCycleSwitcher = gameCycleSwitcher;
            _uiController = uiController;
            _playerSpaceshipManager = playerSpaceshipManager;
            _wavesManager = wavesManager;
            _moneyStarsManager = moneyStarsManager;
            _enemySpaceshipsManager = enemySpaceshipsManager;
            _missionStarsController = missionStarsController;
            
            _uiController.OnScreenSwitch += OnScreenSwitch;
            _playerSpaceshipManager.OnPlayerDie += OnPlayerDie;
            _wavesManager.OnWavesEnd += OnWavesEnd;
        }

        private void OnWavesEnd()
        {
            _wavesManager.OnWavesEnd -= OnWavesEnd;
            if (_enemySpaceshipsManager.ActiveEnemiesCount > 0)
            {
                _enemySpaceshipsManager.OnAllEnemiesGone += OnMissionCompleted;
                return;
            }

            OnMissionCompleted();
        }
        
        private void OnMissionCompleted()
        {
            _missionStarsController.OnMissionCompleted();
            _gameCycleSwitcher.SwitchState(GameCycleState.Pause);
            _moneyStarsManager.ApplyMoneyStars();
            _uiController.SetScreen(ScreenType.GameplayMissionEnd);
        }
        
        private void OnPlayerDie()
        {
            _missionStarsController.OnMissionLoosed();
            _gameCycleSwitcher.SwitchState(GameCycleState.Pause);
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
            _gameCycleSwitcher.SwitchState(GameCycleState.Pause);
        }

        private void OnDeactivatePause()
        {
            if (_playerSpaceshipManager.PlayerIsDead)
            {
                Debug.LogWarning($"Player is dead");
                return;
            }
            
            _gameCycleSwitcher.SwitchState(GameCycleState.Gameplay);
        }

        protected override void OnDispose()
        {
            if(_uiController != null) _uiController.OnScreenSwitch -= OnScreenSwitch;
            if (_playerSpaceshipManager != null) _playerSpaceshipManager.OnPlayerDie -= OnPlayerDie;
            if (_wavesManager != null) _wavesManager.OnWavesEnd -= OnWavesEnd; 
            if (_enemySpaceshipsManager != null) _enemySpaceshipsManager.OnAllEnemiesGone -= OnMissionCompleted;
        }
    }
}