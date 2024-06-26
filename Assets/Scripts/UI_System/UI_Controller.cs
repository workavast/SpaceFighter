using System;
using System.Collections.Generic;
using UI_System.UI_Screens;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI_System
{
    public class UI_Controller : MonoBehaviour
    {
        private List<GameObject> _uiPrevActive = new();
        private List<GameObject> _uiActive = new();
        private List<GameObject> _buffer = new();

        public event Action<ScreenType> OnScreenSwitch;

        private void Start()
        {
            var activeScreens = new List<GameObject>();
            foreach (var screen in UI_ScreenRepository.Screens)
                if (screen.isActiveAndEnabled) activeScreens.Add(screen.gameObject);

            _uiActive = activeScreens;
            if (_uiActive.Count <= 0) Debug.LogWarning("No have active screen");
        
            _uiPrevActive = _uiActive;
        }

        // public void SwitchScreen(ScreensEnum screens, bool setActive)
        // {
        //     UI_ScreenBase addScreen = UI_ScreenRepository.GetScreen(screens);
        //     
        //     if (addScreen.isActiveAndEnabled != setActive)
        //         addScreen.gameObject.SetActive(setActive);
        // }
    
        public void SetScreen(ScreenType screen)
        {
            for (int i = 0; i < _uiActive.Count; i++)
                _uiActive[i].SetActive(false);

            _uiActive = new List<GameObject>();
        
            UI_ScreenBase newScreen = UI_ScreenRepository.GetScreen(screen);
            newScreen.gameObject.SetActive(true);
            _uiActive.Add(newScreen.gameObject);
            OnScreenSwitch?.Invoke(screen);
        }    
    
        // public void SetScreens(List<ScreensEnum> screens)
        // {
        //     var newActiveScreens = new List<GameObject>();
        //     
        //     foreach (var screen in UI_Activies) screen.gameObject.SetActive(false);
        //     foreach (var screen in screens)
        //     {
        //         UI_ScreenBase newScreen = UI_ScreenRepository.GetScreen(screen);
        //         if (!newScreen.isActiveAndEnabled) newScreen.gameObject.SetActive(true);
        //         newActiveScreens.Add(newScreen.gameObject);
        //     }
        //
        //     UI_PrevActivies = UI_Activies;
        //     UI_Activies = newActiveScreens;
        // }

        public void LoadScene(int sceneBuildIndex)
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

        public void Quit()
        {
            Application.Quit();
        }
    }
}