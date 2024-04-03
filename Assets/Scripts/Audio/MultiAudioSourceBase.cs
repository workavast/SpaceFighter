using EventBusExtension;
using UnityEngine;
using Zenject;

namespace Audio
{
    public abstract class MultiAudioSourceBase<TEvent> : PauseableAudioSource, IEventReceiver<TEvent>
        where TEvent : struct, IEvent
    {
        [Inject] private readonly EventBus _eventBus;
        
        public EventBusReceiverIdentifier EventBusReceiverIdentifier { get; } = new();

        private AudioClip _audioClip;
        
        protected override void OnAwake()
        {
            _audioClip = AudioSource.clip;
            _eventBus.Subscribe(this);
        }
        
        private void OnDestroy()
        {
            _eventBus.UnSubscribe(this);
            OnDestroyEvent();
        }

        protected virtual void OnDestroyEvent() { }
        
        public void OnEvent(TEvent @event) => PlayOneShot();
        
        private void PlayOneShot() => AudioSource.PlayOneShot(_audioClip);
    }
}