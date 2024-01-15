using System;

namespace DefaultNamespace
{
    public class Disposable : IDisposable
    {
        protected bool Disposed;
        
        public void Dispose()
        {
            if(Disposed) return;

            OnDispose();
            
            Disposed = true;
        }

        protected virtual void OnDispose()
        {
            
        }
    }
}