using UnityEngine;
using Zenject;

namespace UI_System.UI_Screens
{
    public abstract class UI_ScreenBase : MonoBehaviour
    {
        [Inject] protected readonly UI_Controller UIController;
    
        public virtual void _SetScreen(int screen)
        {
            UIController.SetScreen((ScreenType)screen);
        }
    
        // public virtual void _ActivateScreen(int screen)
        // {
        //     UIController.SwitchScreen((ScreensEnum)screen, true);
        // }
        //
        // public virtual void _DeactivateScreen(int screen)
        // {
        //     UIController.SwitchScreen((ScreensEnum)screen, false);
        // }
    
        public virtual void _LoadScene(int sceneBuildIndex)
        {
            UIController.LoadScene(sceneBuildIndex);
        }

        public virtual void _Quit()
        {
            UIController.Quit();
        }
    }
}