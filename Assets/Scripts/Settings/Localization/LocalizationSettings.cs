using System;

namespace Settings.Localization
{
    public class LocalizationSettings : ISettings
    {
        public int LocalizationId { get; private set; }
        
        public event Action OnChange;

        public LocalizationSettings()
        {
            LocalizationId = 1;
        }
        
        public void ChangeLocalization(int newLocalizationId)
        {
            LocalizationId = newLocalizationId;
            OnChange?.Invoke();
        }

        public void SetData(LocalizationSettingsSave settingsSave) 
            => LocalizationId = settingsSave.LocalizationId;
    }
}