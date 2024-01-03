using System;
using UnityEngine;
using PoolSystem;

public abstract class ProjectileBase<TEnum, TScript> : MonoBehaviour, IPoolable<TScript, TEnum>, IPlayAreaCollision, IHandleUpdate
    where TScript : ProjectileBase<TEnum, TScript>
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float damage;
    
    public abstract TEnum PoolId { get; }
    
    public event Action<TScript> OnLifeTimeEnd;
    public event Action<TScript> OnDestroyElementEvent;
    
    protected abstract bool DestroyableOnCollision { get; }
    protected abstract bool ReturnInPoolOnExitFromPlayArea { get; }
    
    protected event Action OnElementExtractFromPoolEvent;
    protected event Action OnElementReturnInPoolEvent;
    
    public void HandleUpdate(float time) => Move(time);

    protected virtual void Move(float time)
    {
        transform.Translate(Vector3.up * (moveSpeed * time));
    }
    
    public void OnExtractFromPool()
    {
        gameObject.SetActive(true);
        OnElementExtractFromPoolEvent?.Invoke();
    }

    public void OnReturnInPool()
    {
        OnElementReturnInPoolEvent?.Invoke();
        gameObject.SetActive(false);
    }

    public void HandleReturnInPool() => OnLifeTimeEnd?.Invoke((TScript)this);
    
    public void EnterInPlayArea() { }

    public void ExitFromPlayerArea()
    {
        if(ReturnInPoolOnExitFromPlayArea) HandleReturnInPool();
    }

    private void OnDisable() => OnLifeTimeEnd?.Invoke((TScript)this);
    
    private void OnDestroy() => OnDestroyElementEvent?.Invoke((TScript)this);
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out IDamageable iDamageable))
        {
            iDamageable.TakeDamage(damage);

            if (DestroyableOnCollision) HandleReturnInPool();
        }
    }
}
