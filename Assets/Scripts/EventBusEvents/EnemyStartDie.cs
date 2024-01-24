using EventBusExtension;
using UnityEngine;

namespace EventBusEvents
{
    public struct EnemyStartDie : IEvent
    {
        public Vector3 Position { get; private set; }
        
        public EnemyStartDie(Vector3 position)
        {
            Position = position;
        }
    }
}