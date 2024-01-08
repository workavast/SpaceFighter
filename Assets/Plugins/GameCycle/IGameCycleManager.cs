namespace GameCycle
{
    public interface IGameCycleManager
    {
        public GameStatesType CurrentState { get; }
        
        public void AddListener(GameStatesType state, IGameCycleUpdate iGameCycleUpdate);
        public void AddListener(GameStatesType state, IGameCycleFixedUpdate iGameCycleFixedUpdate);
        public void AddListener(GameStatesType state, IGameCycleEnter iGameCycleEnter);
        public void AddListener(GameStatesType state, IGameCycleExit iGameCycleExit); 

        public void RemoveListener(GameStatesType state, IGameCycleUpdate iGameCycleUpdate);
        public void RemoveListener(GameStatesType state, IGameCycleFixedUpdate iGameCycleFixedUpdate);
        public void RemoveListener(GameStatesType state, IGameCycleEnter iGameCycleEnter);
        public void RemoveListener(GameStatesType state, IGameCycleExit iGameCycleExit);
    }
}