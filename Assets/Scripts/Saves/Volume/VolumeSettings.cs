using Saves.Savers;

namespace Saves.Volume
{
    public sealed class VolumeSettings : SettingsBase<VolumeData, VolumeSave>
    {
        protected override string SaveKey => "VolumeSettings";
        
        public float MasterVolume => Data.MasterVolume;
        public float OstVolume => Data.OstVolume;
        public float EffectsVolume => Data.EffectsVolume;

        public VolumeSettings(ISaver saver) : base(saver) { }
        
        public void ChangeMasterVolume(float newVolume)
        {
            Data.MasterVolume = newVolume;
            Save();
        }
    
        public void ChangeOstVolume(float newVolume)
        {
            Data.OstVolume = newVolume;
            Save();
        }
    
        public void ChangeEffectsVolume(float newVolume)
        {
            Data.EffectsVolume = newVolume;
            Save();
        }
    }
}