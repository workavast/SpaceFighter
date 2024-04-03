using Managers;
using Settings;
using Zenject;

namespace UI_System.UI_Screens.MainMenu
{
    public class MainMenuSettingsScreen : UI_ScreenBase
    {
        [Inject] private readonly LocalizationManager _localizationManager;
        
        public void _ChangeLocalization(int localizationId) 
            => _localizationManager.ChangeLocalization(localizationId);

        public void _ResetSaves()
            => PlayerGlobalData.ResetSaves();
    }
}
