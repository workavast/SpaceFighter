using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class PauseableAudioSource : MonoBehaviour
    {
        protected AudioSource _audioSource;

        protected bool IsGlobalPaused;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            OnAwake();
        }

        protected virtual void OnAwake() { }
        
        public void ChangeAudioState(bool isActive)
        {
            IsGlobalPaused = !isActive;
            ChangeAudioState();
        }

        protected virtual void ChangeAudioState()
        {
            if(IsGlobalPaused) _audioSource.Pause();
            else _audioSource.UnPause();
        }
    }
}