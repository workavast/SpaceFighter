﻿namespace GameCycle
{
    public interface IGameCycleSwitcher
    {
        public GameCycleState CurrentState { get; }

        public void SwitchState(GameCycleState newCycleState);
    }
}