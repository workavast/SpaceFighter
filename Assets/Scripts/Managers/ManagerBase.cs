using GameCycle;
using UnityEngine;
using Zenject;

namespace Managers
{
    public abstract class ManagerBase : MonoBehaviour, IGameCycleUpdate
    {
        [Inject] private IGameCycleManager _gameCycleManager;
        protected abstract GameStatesType GameStatesType { get; } 
        
        private void Awake()
        {
            _gameCycleManager.AddListener(GameStatesType, this as IGameCycleUpdate);
            
            OnAwake();
        }
        
        protected virtual void OnAwake() { }
        
        public virtual void GameCycleUpdate() { }

        private void OnDestroy()
        {
            _gameCycleManager.RemoveListener(GameStatesType, this as IGameCycleUpdate);
            OnDestroyVirtual();
        }

        protected virtual void OnDestroyVirtual() { }
    }
}