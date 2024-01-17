using UnityEngine.Localization.Settings;

namespace Managers
{
    public class LocalizationManager : ManagerBase
    {
        private bool _active;

        public void ChangeLocalization(int localizationId)
        {
            if(_active) return;
            
            Change(localizationId);
        }

        private async void Change(int localizationId)
        {
            _active = true;

            var handleTask = LocalizationSettings.InitializationOperation;
            await handleTask.Task;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localizationId];
            
            _active = false;
        } 
    }
}