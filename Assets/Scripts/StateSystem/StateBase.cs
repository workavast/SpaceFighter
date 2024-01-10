using System;

namespace StateSystem
{
    public abstract class StateBase<T> : IHandleUpdate
        where T : Enum
    {
        public abstract T State { get; }

        public virtual void Enter()
        {
            
        }
        
        public virtual void HandleUpdate(float time)
        {

        }

        public virtual void Exit()
        {
            
        }
    }
}