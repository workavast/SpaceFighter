using System;

namespace Saves.Localization
{
    [Serializable]
    public sealed class LocalizationSettingsSave
    {
        public int LocalizationId;
        
        public LocalizationSettingsSave(LocalizationSettings settings)
            => LocalizationId = settings.LocalizationId;
    }
}