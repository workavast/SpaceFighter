using Saves;
using UnityEngine;
using UnityEngine.Localization.Settings;
using YG;

namespace Initializers
{
    public class LocalizationInitializer : InitializerBase
    {
        public LocalizationInitializer(InitializerBase[] initializers = null) 
            : base(initializers) { }

        public override void Init() => InitLocalizationSettings();

        private async void InitLocalizationSettings()
        {
            var handleTask = LocalizationSettings.InitializationOperation;
            await handleTask.Task;

            int langIndex = 1;
            if (YandexGame.savesData.isFirstSession)
            {
                switch (YandexGame.lang)
                {
                    case "ru":
                        langIndex = 0;
                        break;
                    case "tr":
                        langIndex = 2;
                        break;
                    default://en
                        langIndex = 1;
                        break;
                }
            }
            else
            {
                langIndex = PlayerGlobalData.Instance.LocalizationSettings.LocalizationId;
            }
            
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[langIndex];
            
            var handleTask2 = LocalizationSettings.InitializationOperation;
            await handleTask2.Task;
            
            Debug.Log("-||- LocalizationInitializer");
            OnParentInit?.Invoke();
        }
    }
}
