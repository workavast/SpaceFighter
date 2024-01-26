namespace Saves.Volume
{
    public class VolumeData : DataBase<VolumeSave>
    {
        public float MasterVolume;
        public float OstVolume;
        public float EffectsVolume;

        public VolumeData()
        {
            MasterVolume = 0.5f;
            OstVolume = 0.5f;
            EffectsVolume = 0.5f;
        }
            
        public VolumeData(float masterVolume, float ostVolume, float effectsVolume)
        {
            MasterVolume = masterVolume;
            OstVolume  = ostVolume;
            EffectsVolume = effectsVolume;
        }

        public override void SetData(VolumeSave volume)
        {
            MasterVolume = volume.MasterVolume;
            OstVolume = volume.OstVolume;
            EffectsVolume = volume.EffectsVolume;
        }
    }
}