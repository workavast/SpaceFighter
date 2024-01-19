using System;

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