using GameCycle;
using Zenject;

namespace Managers
{
    public abstract class GameCycleManager : ManagerBase, IGameCycleUpdate
    {
        [Inject] protected IGameCycleController GameCycleController;
        protected abstract GameCycleState GameCycleState { get; } 
        
        protected override void OnAwake()
        {
            GameCycleController.AddListener(GameCycleState, this as IGameCycleUpdate);
        }
        
        public virtual void GameCycleUpdate() { }

        private void OnDestroy()
        {
            GameCycleController.RemoveListener(GameCycleState, this as IGameCycleUpdate);
            OnDestroyVirtual();
        }
        
        protected virtual void OnDestroyVirtual() { }
    }
}