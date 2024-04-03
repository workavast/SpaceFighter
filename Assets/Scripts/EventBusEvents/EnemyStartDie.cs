using EventBusExtension;
using UnityEngine;

namespace EventBusEvents
{
    public struct EnemyStartDie : IEvent
    {
        public readonly Vector3 Position;
        public readonly float StarsValueScale;
        
        public EnemyStartDie(Vector3 position, float starsValueScale)
        {
            Position = position;
            StarsValueScale = starsValueScale;
        }
    }
}