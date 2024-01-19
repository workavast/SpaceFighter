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
        
            SceneManager.LoadScene(1);
        }
    }
}
