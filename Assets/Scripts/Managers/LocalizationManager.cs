using UnityEngine.Localization.Settings;

namespace Managers
{
    public class LocalizationManager : ManagerBase
    {
        private bool _active;
        
        public async void ChangeLocalization(int localizationId)
        {
            if(_active) return;
            
            _active = true;

            var handleTask = LocalizationSettings.InitializationOperation;
            await handleTask.Task;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localizationId];
            
            _active = false;
        }
    }
}