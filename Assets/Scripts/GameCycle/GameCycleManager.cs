using System;
using System.Collections.Generic;
using System.Linq;
using EnumValuesExtension;
using UnityEngine;

namespace GameCycle
{
    [DefaultExecutionOrder(-10000)]
    public class GameCycleManager : MonoBehaviour, IGameCycleManager, IGameCycleManagerSwitcher
    {
        private readonly Dictionary<GameStatesType, Dictionary<GameCycleInvokeType, Action>> _actions = new ();

        public GameStatesType CurrentState { get; private set; }

        private void Awake()
        {
            CurrentState = GameStatesType.Gameplay;
            var gameStatesTypes = EnumValuesTool.GetValues<GameStatesType>();
            var gameCycleInvokeTypes = EnumValuesTool.GetValues<GameCycleInvokeType>().ToList();
            foreach (var state in gameStatesTypes)
            {
                _actions.Add(state, new Dictionary<GameCycleInvokeType, Action>());
                foreach (var invokeType in gameCycleInvokeTypes)
                    _actions[state].Add(invokeType, null);
            }
        }

        private void Update() => _actions[CurrentState][GameCycleInvokeType.Update]?.Invoke();
        private void FixedUpdate() => _actions[CurrentState][GameCycleInvokeType.FixedUpdate]?.Invoke();

        public void AddListener(GameStatesType state, IGameCycleUpdate iGameCycleUpdate)
            => AddListener(state, GameCycleInvokeType.Update, iGameCycleUpdate.GameCycleUpdate);
        public void AddListener(GameStatesType state, IGameCycleFixedUpdate iGameCycleFixedUpdate) 
            => AddListener(state, GameCycleInvokeType.FixedUpdate, iGameCycleFixedUpdate.GameCycleFixedUpdate);
        public void AddListener(GameStatesType state, IGameCycleEnter iGameCycleEnter)
            => AddListener(state, GameCycleInvokeType.StateEnter, iGameCycleEnter.GameCycleEnter);
        public void AddListener(GameStatesType state, IGameCycleExit iGameCycleExit)
            => AddListener(state, GameCycleInvokeType.StateExit, iGameCycleExit.GameCycleExit);
        
        private void AddListener(GameStatesType state, GameCycleInvokeType invokeType, Action method)
        {
            _actions[state][invokeType] -= method;
            _actions[state][invokeType] += method;
        }

        public void RemoveListener(GameStatesType state, IGameCycleUpdate iGameCycleUpdate) 
            => _actions[state][GameCycleInvokeType.Update] -= iGameCycleUpdate.GameCycleUpdate;
        public void RemoveListener(GameStatesType state, IGameCycleFixedUpdate iGameCycleFixedUpdate) 
            => _actions[state][GameCycleInvokeType.FixedUpdate] -= iGameCycleFixedUpdate.GameCycleFixedUpdate;
        public void RemoveListener(GameStatesType state, IGameCycleEnter iGameCycleEnter)
            => _actions[state][GameCycleInvokeType.StateEnter] -= iGameCycleEnter.GameCycleEnter;
        public void RemoveListener(GameStatesType state, IGameCycleExit iGameCycleExit)
            => _actions[state][GameCycleInvokeType.StateExit] -= iGameCycleExit.GameCycleExit;

        public void SwitchState(GameStatesType newState)
        {
            if(CurrentState == newState) return;
            
            _actions[CurrentState][GameCycleInvokeType.StateExit]?.Invoke();
            CurrentState = newState;
            _actions[CurrentState][GameCycleInvokeType.StateEnter]?.Invoke();
        }
    }
}