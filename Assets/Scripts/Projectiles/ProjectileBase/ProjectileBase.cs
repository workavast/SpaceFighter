using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase<TEnum, TScript> : MonoBehaviour, IPoolable<TScript, TEnum>, IPlayAreaCollision, IHandleUpdate
    where TScript : ProjectileBase<TEnum, TScript>
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] private float damage;
    
    public abstract TEnum PoolId { get; }
    
    public event Action<TScript> ReturnElementEvent;
    public event Action<TScript> DestroyElementEvent;
    
    protected abstract bool DestroyableOnCollision { get; }
    protected abstract bool ReturnInPoolOnExitFromPlayArea { get; }
    
    protected event Action OnElementExtractFromPoolEvent;
    protected event Action OnElementReturnInPoolEvent;
    
    public void HandleUpdate() => Move();

    protected virtual void Move()
    {
        transform.Translate(Vector3.up * (moveSpeed * Time.deltaTime));
    }
    
    public void OnElementExtractFromPool()
    {
        gameObject.SetActive(true);
        OnElementExtractFromPoolEvent?.Invoke();
    }

    public void OnElementReturnInPool()
    {
        OnElementReturnInPoolEvent?.Invoke();
        gameObject.SetActive(false);
    }

    public void ReturnInPool() => ReturnElementEvent?.Invoke((TScript)this);
    
    public void EnterInPlayArea() { }

    public void ExitFromPlayerArea()
    {
        if(ReturnInPoolOnExitFromPlayArea) ReturnInPool();
    }

    private void OnDisable() => ReturnElementEvent?.Invoke((TScript)this);
    
    private void OnDestroy() => DestroyElementEvent?.Invoke((TScript)this);
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out IDamageable iDamageable))
        {
            iDamageable.TakeDamage(damage);

            if (DestroyableOnCollision) ReturnInPool();
        }
    }
}
