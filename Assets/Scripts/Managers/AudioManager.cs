using Audio;
using Settings;
using UnityEngine;
using UnityEngine.Audio;

namespace Managers
{
    public class AudioManager : ManagerBase
    {
        [SerializeField] private AudioMixer mixer;
        
        public AudioVolumeChanger Volume { get; private set; }

        private PauseableAudioSource[] _pauseableAudioSources;

        private void Awake()
        {
            Volume = new AudioVolumeChanger(mixer, PlayerGlobalData.Instance.VolumeSettings);
            _pauseableAudioSources = GetComponentsInChildren<PauseableAudioSource>();
        }
        
        private void Start()
        {
            Volume.StartInit();
        }

        public void ChangeAudioState(bool pausedAudio)
        {
            foreach (var audioSource in _pauseableAudioSources)
                audioSource.ChangeAudioState(pausedAudio);
        }
    }
}