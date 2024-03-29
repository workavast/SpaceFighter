using Managers;
using Saves;
using Zenject;

namespace UI_System.UI_Screens.MainMenu
{
    public class MainMenuSettingsScreen : UI_ScreenBase
    {
        [Inject] private LocalizationManager _localizationManager;
        
        public void _ChangeLocalization(int localizationId) => _localizationManager.ChangeLocalization(localizationId);

        public void _ResetSaves()
            => PlayerGlobalData.Instance.ResetSaves();
    }
}
