using System;

namespace Saves.Volume
{
    [Serializable]
    public sealed class VolumeSettingsSave
    {
        public float MasterVolume = 0.5f;
        public float OstVolume = 0.5f;
        public float EffectsVolume = 0.5f;

        public VolumeSettingsSave()
        {
            MasterVolume = 0.5f;
            OstVolume = 0.5f;
            EffectsVolume = 0.5f;
        }
        
        public VolumeSettingsSave(VolumeSettings settings)
        {
            MasterVolume = settings.MasterVolume;
            OstVolume = settings.OstVolume;
            EffectsVolume = settings.EffectsVolume;
        }
    }
}