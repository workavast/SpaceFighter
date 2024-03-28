using System;

namespace Saves.Volume
{
    [Serializable]
    public sealed class VolumeSettingsSave
    {
        public float MasterVolume;
        public float OstVolume;
        public float EffectsVolume;
        
        public VolumeSettingsSave(VolumeSettings settings)
        {
            MasterVolume = settings.MasterVolume;
            OstVolume = settings.OstVolume;
            EffectsVolume = settings.EffectsVolume;
        }
    }
}