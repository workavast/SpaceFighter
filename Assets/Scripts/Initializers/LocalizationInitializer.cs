using Saves;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Initializers
{
    public class LocalizationInitializer : InitializerBase
    {
        public override void Init() => InitLocalizationSettings();

        public LocalizationInitializer(InitializerBase[] initializers = null) 
            : base(initializers) { }

        private async void InitLocalizationSettings()
        {
            var handleTask = LocalizationSettings.InitializationOperation;
            await handleTask.Task;

            LocalizationSettings.SelectedLocale =
                LocalizationSettings.AvailableLocales.Locales[PlayerGlobalData.LocalizationSettings.LocalizationId];
            
            Debug.Log("LocalizationInitializer");
            OnParentInit?.Invoke();
        }
    }
}
