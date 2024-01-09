﻿using EventBusExtension;

namespace Managers
{
    public class MissionEventBus
    {
        private EventBus EventBus { get; } = new();

        public void Subscribe<T>(IEventReceiver<T> receiver) where T : struct, IEvent
            => EventBus.Subscribe(receiver);
        
        public void UnSubscribe<T>(IEventReceiver<T> receiver) where T : struct, IEvent
            => EventBus.UnSubscribe(receiver);
        
        public void Invoke<T>(T @event) where T : struct, IEvent
            => EventBus.Invoke(@event);
    }
}