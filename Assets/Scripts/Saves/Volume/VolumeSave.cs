namespace Saves.Volume
{
    public class VolumeSave : SaveBase<VolumeData>
    {
        public float MasterVolume;
        public float OstVolume;
        public float EffectsVolume;
        
        public override void SetData(VolumeData volumeData)
        {
            MasterVolume = volumeData.MasterVolume;
            OstVolume = volumeData.OstVolume;
            EffectsVolume = volumeData.EffectsVolume;
        }
    }
}