using GameCycle;
using Zenject;

namespace Managers
{
    public abstract class GameCycleManager : ManagerBase, IGameCycleUpdate
    {
        [Inject] private IGameCycleManager _gameCycleManager;
        protected abstract GameStatesType GameStatesType { get; } 
        
        protected override void OnAwake()
        {
            _gameCycleManager.AddListener(GameStatesType, this as IGameCycleUpdate);
        }
        
        public virtual void GameCycleUpdate() { }

        private void OnDestroy()
        {
            _gameCycleManager.RemoveListener(GameStatesType, this as IGameCycleUpdate);
            OnDestroyVirtual();
        }
        
        protected virtual void OnDestroyVirtual() { }
    }
}