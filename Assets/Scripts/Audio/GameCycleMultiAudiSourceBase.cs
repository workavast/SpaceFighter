using EventBusExtension;
using GameCycle;
using Zenject;

namespace Audio
{
    public abstract class GameCycleMultiAudiSourceBase<T> : MultiAudioSourceBase<T>, IGameCycleEnter, IGameCycleExit
        where T: struct, IEvent
    {
        [Inject] private readonly IGameCycleController _gameCycleController;

        protected abstract GameCycleState GameCycleState { get; }
        
        private bool _gameCycleIsActive;

        protected override void OnAwake()
        {
            base.OnAwake();
            
            _gameCycleController.AddListener(GameCycleState, this as IGameCycleEnter);
            _gameCycleController.AddListener(GameCycleState, this as IGameCycleExit);
        }
        
        protected override void OnDestroyEvent()
        {
            base.OnDestroyEvent();
            
            _gameCycleController.RemoveListener(GameCycleState, this as IGameCycleEnter);
            _gameCycleController.RemoveListener(GameCycleState, this as IGameCycleExit);
        }
        
        public void GameCycleEnter()
        {
            _gameCycleIsActive = true;
            ChangeAudioState();
        }

        public void GameCycleExit()
        {
            _gameCycleIsActive = false;
            ChangeAudioState();
        }
        
        protected override void ChangeAudioState()
        {
            if (IsGlobalPaused || !_gameCycleIsActive) AudioSource.Pause();
            else AudioSource.UnPause();
        }
    }
}