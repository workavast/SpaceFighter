using System;

namespace PoolSystem
{
    public interface IPoolable<TElement>
    {
        public event Action<TElement> ReturnElementEvent;
        public event Action<TElement> DestroyElementEvent;
    
        public void OnExtractFromPool();
        public void OnReturnInPool();
    }

    public interface IPoolable<TElement, TId> : IPoolable<TElement>
    {
        public TId PoolId { get;}
    }
}