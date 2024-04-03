using System;

namespace Settings.Localization
{
    [Serializable]
    public sealed class LocalizationSettingsSave
    {
        public int LocalizationId = 1;
        
        public LocalizationSettingsSave()
        {
            LocalizationId = 1;
        }
        
        public LocalizationSettingsSave(LocalizationSettings settings)
            => LocalizationId = settings.LocalizationId;
    }
}