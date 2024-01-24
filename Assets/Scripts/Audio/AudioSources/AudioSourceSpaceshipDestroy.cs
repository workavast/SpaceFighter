using EventBusEvents;
using GameCycle;

namespace Audio.AudioSources
{
    public class AudioSourceSpaceshipDestroy : GameCycleMultiAudiSourceBase<EnemyStartDie>
    {
        protected override GameCycleState GameCycleState => GameCycleState.Gameplay;
    }
}