namespace GameCycle
{
    public interface IGameCycleManagerSwitcher
    {
        public GameStatesType CurrentState { get; }

        public void SwitchState(GameStatesType newState);
    }
}