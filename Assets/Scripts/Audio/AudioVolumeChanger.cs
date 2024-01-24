using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class AudioVolumeChanger
    {
        private readonly AudioMixer _mixer;
        private readonly VolumeSaveDataController _volumeSaveDataController;

        private const string MasterParam = "Master";
        private const string EffectsParam = "Effects";
        private const string OstParam = "Ost";

        public AudioVolumeChanger(AudioMixer mixer, VolumeSaveDataController volumeSaveDataController)
        {
            _mixer = mixer;
            _volumeSaveDataController = volumeSaveDataController;
        }

        public float MasterVolume => _volumeSaveDataController.MasterVolume;
        public float OstVolume => _volumeSaveDataController.OstVolume;
        public float EffectsVolume => _volumeSaveDataController.EffectsVolume;

        public void StartInit()
        {
            SetVolume(MasterParam, MasterVolume);
            SetVolume(EffectsParam, EffectsVolume);
            SetVolume(OstParam, OstVolume);
        }

        
        public void SetMasterVolume(float newVolume)
        {
            _volumeSaveDataController.ChangeMasterVolume(newVolume);
            SetVolume(MasterParam, MasterVolume);
        }

        public void SetEffectsVolume(float newVolume)
        {
            _volumeSaveDataController.ChangeEffectsVolume(newVolume);
            SetVolume(EffectsParam, EffectsVolume);
        }

        public void SetOstVolume(float newVolume)
        {
            _volumeSaveDataController.ChangeOstVolume(newVolume);
            SetVolume(OstParam, OstVolume);
        }

        private void SetVolume(string paramName, float newVolume)
            => _mixer.SetFloat($"{paramName}", Mathf.Lerp(-80, 0, Mathf.Sqrt(Mathf.Sqrt(newVolume))));
    }
}