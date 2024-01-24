using GameCycle;
using UnityEngine;
using Zenject;

namespace Audio
{
    public class GameCycleSingleAudioSource : SingleAudioSource, IGameCycleEnter, IGameCycleExit
    {
        [SerializeField] [Range(0, 1)] private float volume;
        [SerializeField] private GameCycleState gameCycleState;
        
        [Inject] private IGameCycleController _gameCycleController;

        private float _startVolume;

        protected override void OnAwake()
        {
            base.OnAwake();

            _startVolume = _audioSource.volume;
            
            _gameCycleController.AddListener(gameCycleState, this as IGameCycleEnter);
            _gameCycleController.AddListener(gameCycleState, this as IGameCycleExit);

            if (_gameCycleController.CurrentState != gameCycleState)
                SetVolume(false);
        }

        public void GameCycleEnter() => SetVolume(true);

        public void GameCycleExit() => SetVolume(false);

        private void SetVolume(bool isCurrentState) => _audioSource.volume = isCurrentState ? _startVolume : volume;
    }
}