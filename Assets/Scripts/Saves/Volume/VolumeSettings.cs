using System;

namespace Saves.Volume
{
    public sealed class VolumeSettings : ISettings
    {
        public float MasterVolume { get; private set; }
        public float OstVolume { get; private set; }
        public float EffectsVolume { get; private set; }
        
        public event Action OnChange;

        public VolumeSettings()
        {
            MasterVolume = 0.5f;
            OstVolume = 0.5f;
            EffectsVolume = 0.5f;
        }
            
        public VolumeSettings(float masterVolume, float ostVolume, float effectsVolume)
        {
            MasterVolume = masterVolume;
            OstVolume  = ostVolume;
            EffectsVolume = effectsVolume;
        }
        
        public void ChangeMasterVolume(float newVolume)
        {
            MasterVolume = newVolume;
            OnChange?.Invoke();
        }
    
        public void ChangeOstVolume(float newVolume)
        {
            OstVolume = newVolume;
            OnChange?.Invoke();
        }
    
        public void ChangeEffectsVolume(float newVolume)
        {
            EffectsVolume = newVolume;
            OnChange?.Invoke();
        }
        
        public void LoadData(VolumeSettingsSave volumeSettings)
        {
            MasterVolume = volumeSettings.MasterVolume;
            OstVolume = volumeSettings.OstVolume;
            EffectsVolume = volumeSettings.EffectsVolume;
        }
    }
}