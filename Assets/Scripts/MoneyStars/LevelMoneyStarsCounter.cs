using UI_System;
using UI_System.UI_Screens.Gameplay;
using UnityEngine;

public class LevelMoneyStarsCounter : MonoBehaviour
{
    public static LevelMoneyStarsCounter _instance;

    private GameplayMainScreen _gameplayMainScreen;
    private int _value = 0;
    
    private void Awake()
    {
        if (_instance)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        _gameplayMainScreen = UI_ScreenRepository.GetScreen(ScreensEnum.GameplayMain) as GameplayMainScreen;
        _instance._gameplayMainScreen.UpdateLevelMoneyCount(_instance._value);
    }

    public static void ChangeValue(int value)
    {
        _instance._value += value;
        _instance._gameplayMainScreen.UpdateLevelMoneyCount(_instance._value);
    }

    public static void ApplyValue()
    {
        PlayerGlobalData.ChangeMoneyStarsCount(_instance._value);
    }
}
