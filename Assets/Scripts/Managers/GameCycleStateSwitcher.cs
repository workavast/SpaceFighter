using GameCycle;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class GameCycleStateSwitcher : MonoBehaviour
    {
        [Inject] private IGameCycleManagerSwitcher _gameCycleManager;
        [Inject] private PlayerSpaceshipManager _playerSpaceshipManager;
        [Inject] private UI_Controller _uiController;
        [Inject] private MissionController _missionController;
        
        private void Awake()
        {
            _playerSpaceshipManager.OnPlayerDie += SetPauseState;
            _uiController.OnScreenSwitch += UI_Switch;
            _missionController.OmMissionCompleted += SetPauseState;
        }

        private void UI_Switch(ScreensEnum screen)
        {
            switch (screen)
            {
                case ScreensEnum.GameplayMainScreen:
                    SetGameplayState();
                    break;
                case ScreensEnum.GameplayMenuScreen:
                    SetPauseState();
                    break;
                default:
                    return;
            }
        }
        
        private void SetGameplayState() => _gameCycleManager.SwitchState(GameStatesType.Gameplay);
        private void SetPauseState() => _gameCycleManager.SwitchState(GameStatesType.Pause);
    }
}