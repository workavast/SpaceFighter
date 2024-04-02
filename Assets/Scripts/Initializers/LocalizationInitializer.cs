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
            if (PlayerGlobalData.Instance.IsFirstSession)
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
                Debug.Log($"-||- InitLocalization isFirstSession: {YandexGame.lang} | {langIndex}");
            }
            else
            {
                Debug.Log($"-||- InitLocalization not first: {YandexGame.lang} | {langIndex}");
                langIndex = PlayerGlobalData.Instance.LocalizationSettings.LocalizationId;
            }
            
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[langIndex];
            
            var handleTask2 = LocalizationSettings.InitializationOperation;
            await handleTask2.Task;
            PlayerGlobalData.Instance.LocalizationSettings.ChangeLocalization(langIndex);

            Debug.Log("-||- LocalizationInitializer");
            OnParentInit?.Invoke();
        }
    }
}
