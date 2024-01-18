using System;
using UnityEngine;
using SomeStorages;

[RequireComponent(typeof(PolygonCollider2D), typeof(Rigidbody2D))]
public abstract class SpaceshipBase : MonoBehaviour, IDamageable, IHandleUpdate
{
    [SerializeField] protected SomeStorageFloat healthPoints;
    
    public event Action OnDead;
    
    protected bool IsDead = false;
    
    private void Awake() => OnAwake();
    private void Start() => OnStart();

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    
    public abstract void ChangeAnimatorState(bool animatorEnabled);
    
    public virtual void TakeDamage(float damage)
    {
        healthPoints.ChangeCurrentValue(-damage);

        if (healthPoints.IsEmpty)
            OnDead?.Invoke();
    }

    public virtual void HandleUpdate(float time) { }
}
