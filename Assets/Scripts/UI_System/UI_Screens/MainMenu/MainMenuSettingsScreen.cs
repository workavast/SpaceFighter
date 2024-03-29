using Managers;
using Zenject;

namespace UI_System.UI_Screens.MainMenu
{
    public class MainMenuSettingsScreen : UI_ScreenBase
    {
        [Inject] private LocalizationManager _localizationManager;
        
        public void _ChangeLocalization(int localizationId) => _localizationManager.ChangeLocalization(localizationId);    
    }
}
