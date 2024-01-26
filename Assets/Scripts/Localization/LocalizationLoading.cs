using Saves;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

namespace Localization
{
    public class LocalizationLoading : MonoBehaviour
    {
        private void Start() => InitLocalizationSettings();
    
        private async void InitLocalizationSettings()
        {
            var handleTask = LocalizationSettings.InitializationOperation;
            await handleTask.Task;

            LocalizationSettings.SelectedLocale =
                LocalizationSettings.AvailableLocales.Locales[PlayerGlobalData.LocalizationSettings.LocalizationId];
            
            SceneManager.LoadScene(1);
        }
    }
}
