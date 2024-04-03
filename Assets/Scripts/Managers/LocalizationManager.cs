using System.Threading.Tasks;
using Settings;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Managers
{
    public class LocalizationManager : ManagerBase
    {
        private bool _active;
        
        public async void ChangeLocalization(int localizationId)
        {
            if(_active || PlayerGlobalData.Instance.LocalizationSettings.LocalizationId == localizationId)
                return;
            
            if (localizationId >= LocalizationSettings.AvailableLocales.Locales.Count || localizationId < 0)
            {
                Debug.LogError("Invalid localization Id");
                return;
            }
            
            await ApplyLocalization(localizationId);
            
            PlayerGlobalData.Instance.LocalizationSettings.ChangeLocalization(localizationId);
        }

        private async Task ApplyLocalization(int localizationId)
        {
            _active = true;

            var handleTask = LocalizationSettings.InitializationOperation;
            await handleTask.Task;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localizationId];
            
            _active = false;
        }
    }
}