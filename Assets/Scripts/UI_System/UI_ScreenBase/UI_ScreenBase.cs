using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ScreenBase : MonoBehaviour
{
    public virtual void _SetScreen(int screen)
    {
        UI_Controller.SetScreen((ScreenEnum)screen);
    }
    
    public virtual void _ActivateScreen(int screen)
    {
        UI_Controller.SwitchScreen((ScreenEnum)screen, true);
    }
    
    public virtual void _DeactivateScreen(int screen)
    {
        UI_Controller.SwitchScreen((ScreenEnum)screen, false);
    }
    
    public virtual void _LoadScene(int sceneBuildIndex)
    {
        UI_Controller.LoadScene(sceneBuildIndex);
    }

    public virtual void _Quit()
    {
        UI_Controller.Quit();
    }
}