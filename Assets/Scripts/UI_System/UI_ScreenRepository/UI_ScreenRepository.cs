using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
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
    public static UI_ScreenBase GetScreenByEnum(ScreenEnum screenEnum)
    {
        if (Instance == null) throw new NullReferenceException($"Instance is null");
        
        switch (screenEnum)
        {
            case ScreenEnum.FirstScreen:
                return GetScreen<UI_FirstScreen>();
            case ScreenEnum.SecondScreen:
                return GetScreen<UI_SecondScreen>();
            case ScreenEnum.ThirdScreen:
                return GetScreen<UI_ThirdScreen>();
            default:
                Debug.LogWarning("Error: invalid parameter in SetWindow(ScreenEnum screen)");
                return default;
        }
    }
}