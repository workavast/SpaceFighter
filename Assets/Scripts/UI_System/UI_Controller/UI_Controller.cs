using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    private List<GameObject> UI_Activies = new List<GameObject>();
    private List<GameObject> UI_PrevActivies = new List<GameObject>();
    private List<GameObject> buffer = new List<GameObject>();

    private static UI_Controller _instance;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    void Start()
    {
        var activeScreens = new List<GameObject>();
        foreach (var screen in UI_ScreenRepository.Screens)
            if (screen.isActiveAndEnabled) activeScreens.Add(screen.gameObject);

        UI_Activies = activeScreens;
        if (UI_Activies.Count <= 0) Debug.LogError("No have active screen");
        
        UI_PrevActivies = UI_Activies;
    }

    public static void SwitchScreen(ScreensEnum screens, bool setActive)
    {
        UI_ScreenBase addScreen = UI_ScreenRepository.GetScreenByEnum(screens);
        
        if (addScreen.isActiveAndEnabled != setActive)
            addScreen.gameObject.SetActive(setActive);
    }
    
    public static void SetScreen(ScreensEnum screens)
    {
        for (int i = 0; i < _instance.UI_Activies.Count; i++)
            _instance.UI_Activies[i].SetActive(false);

        _instance.UI_Activies = new List<GameObject>();
        
        UI_ScreenBase addScreen = UI_ScreenRepository.GetScreenByEnum(screens);
        addScreen.gameObject.SetActive(true);
        _instance.UI_Activies.Add(addScreen.gameObject);
    }    
    public static void SetScreens(List<ScreensEnum> screens)
    {
        var newActiveScreens = new List<GameObject>();
        
        foreach (var screen in _instance.UI_Activies) screen.gameObject.SetActive(false);
        foreach (var screen in screens)
        {
            UI_ScreenBase newScreen = UI_ScreenRepository.GetScreenByEnum(screen);
            if (!newScreen.isActiveAndEnabled) newScreen.gameObject.SetActive(true);
            newActiveScreens.Add(newScreen.gameObject);
        }

        _instance.UI_PrevActivies = _instance.UI_Activies;
        _instance.UI_Activies = newActiveScreens;
    }

    public static void LoadScene(int sceneBuildIndex)
    {
        if (sceneBuildIndex == -1)
        {
            int currentSceneNum = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneNum, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }

    public static void Quit()
    {
        Application.Quit();
    }
}