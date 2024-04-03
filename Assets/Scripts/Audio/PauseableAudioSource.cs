using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class PauseableAudioSource : MonoBehaviour
    {
        protected AudioSource AudioSource;

        protected bool IsGlobalPaused;
        
        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
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
            if(IsGlobalPaused) AudioSource.Pause();
            else AudioSource.UnPause();
        }
    }
}