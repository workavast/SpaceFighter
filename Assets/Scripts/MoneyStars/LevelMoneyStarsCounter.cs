using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelMoneyStarsCounter : MonoBehaviour
{
    public static LevelMoneyStarsCounter Instance;

    private GameplayMainScreen _gameplayMainScreen;
    private int _value = 0;
    
    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _gameplayMainScreen = UI_ScreenRepository.GetScreenByEnum(ScreensEnum.GameplayMainScreen) as GameplayMainScreen;
        Instance._gameplayMainScreen.UpdateLevelMoneyCount(Instance._value);
    }

    public static void ChangeValue(int value)
    {
        Instance._value += value;
        Instance._gameplayMainScreen.UpdateLevelMoneyCount(Instance._value);
    }
}
