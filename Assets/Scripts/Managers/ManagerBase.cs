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

        private void Start() => OnStart();
        
        protected virtual void OnAwake() { }
        
        protected virtual void OnStart() { }
        
        public virtual void GameCycleUpdate() { }
    }
}