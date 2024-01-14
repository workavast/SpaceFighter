using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UI_System.UI_Screens;
using UI_System.UI_Screens.MainMenu;

public class UI_ScreenRepository : MonoBehaviour
{
    private Dictionary<Type, UI_ScreenBase> _screens;

    private static UI_ScreenRepository _instance;
    public static IReadOnlyList<UI_ScreenBase> Screens => _instance._screens.Values.ToArray();

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }

        _instance = this;

        _screens = new Dictionary<Type, UI_ScreenBase>();

        UI_ScreenBase[] uiScreens = FindObjectsOfType<UI_ScreenBase>(true);
        foreach (UI_ScreenBase screen in uiScreens) _screens.Add(screen.GetType(), screen);
    }

    public static TScreen GetScreen<TScreen>() where TScreen : UI_ScreenBase
    {
        if (_instance == null) throw new NullReferenceException($"Instance is null");

        if (!_instance._screens.TryGetValue(typeof(TScreen), out UI_ScreenBase screen))
        {
            Debug.LogWarning("Error: invalid parameter in SetWindow(ScreenEnum screen)");
            return default;
        }

        return (TScreen)screen;
    }
    
    public static UI_ScreenBase GetScreen(ScreensEnum screensEnum)
    {
        if (_instance == null) throw new NullReferenceException($"Instance is null");
        
        switch (screensEnum)
        {     
            case ScreensEnum.MainMenuHangar:
                return GetScreen<MainMenuHangarScreen>();
            case ScreensEnum.MainMenuLevelSelection:
                return GetScreen<MainMenuMissionSelectionScreen>();
            case ScreensEnum.MainMenuSettings:
                return GetScreen<MainMenuSettingsScreen>();
            case ScreensEnum.GameplayMain:
                return GetScreen<GameplayMainScreen>();
            case ScreensEnum.GameplayMenu:
                return GetScreen<GameplayMenuScreen>();
            case ScreensEnum.GameplayMissionEnd:
                return GetScreen<GameplayMissionEndScreen>();
            default:
                Debug.LogWarning("Error: invalid parameter in GetScreenByEnum(ScreenEnum screen)");
                return default;
        }
    }
}