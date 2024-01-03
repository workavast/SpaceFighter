using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class UI_ScreenRepository : MonoBehaviour
{
    private Dictionary<Type, UI_ScreenBase> _screens;
    
    public static UI_ScreenRepository Instance;
    public static IReadOnlyList<UI_ScreenBase> Screens => Instance._screens.Values.ToArray();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        _screens = new Dictionary<Type, UI_ScreenBase>();

        UI_ScreenBase[] uiScreens = FindObjectsOfType<UI_ScreenBase>(true);
        foreach (UI_ScreenBase screen in uiScreens) _screens.Add(screen.GetType(), screen);
    }

    public static TScreen GetScreen<TScreen>() where TScreen : UI_ScreenBase
    {
        if (Instance == null) throw new NullReferenceException($"Instance is null");
        
        if(Instance._screens.TryGetValue(typeof(TScreen), out UI_ScreenBase screen)) return (TScreen)screen;

        return default;
    }
    
    public static UI_ScreenBase GetScreenByEnum(ScreensEnum screensEnum)
    {
        if (Instance == null) throw new NullReferenceException($"Instance is null");
        
        switch (screensEnum)
        {     
            case ScreensEnum.MainMenuHangarScreen:
                return GetScreen<MainMenuHangarScreen>();
            case ScreensEnum.MainMenuLevelSelectionScreen:
                return GetScreen<MainMenuMissionSelectionScreen>();
            case ScreensEnum.MainMenuSettingsScreen:
                return GetScreen<MainMenuSettingsScreen>();
            case ScreensEnum.GameplayMainScreen:
                return GetScreen<GameplayMainScreen>();
            case ScreensEnum.GameplayMenuScreen:
                return GetScreen<GameplayMenuScreen>();
            default:
                Debug.LogWarning("Error: invalid parameter in SetWindow(ScreenEnum screen)");
                return default;
        }
    }
}