using System;
using UnityEngine;

public class MissionCycleController : MonoBehaviour
{
    public enum MissionStatesEnum
    {
        Gameplay,
        Pause,
        Loose,
        Win
    }

    private MissionStatesEnum _currentMissionStatesEnum;

    public event Action OnGameplayState;
    public event Action OnPauseState;
    public event Action OnLooseState;
    public event Action OnWinState;

    public void ChangeMissionState(MissionStatesEnum newMissionStatesEnum)
    {
        switch (newMissionStatesEnum)
        {
            case MissionStatesEnum.Gameplay:
                OnGameplayState?.Invoke();
                break;
            case MissionStatesEnum.Pause:
                OnPauseState?.Invoke();
                break;
            case MissionStatesEnum.Loose:
                OnLooseState?.Invoke();
                break;
            case MissionStatesEnum.Win:
                OnWinState?.Invoke();
                break;
            default:
                Debug.Log("Undefined MissionStatesEnum");
                return;
        }

        _currentMissionStatesEnum = newMissionStatesEnum;
    }
}
