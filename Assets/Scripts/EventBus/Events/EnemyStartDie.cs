using EventBusExtension;
using UnityEngine;

namespace EventBus.Events
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