using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using SomeStorages;

[RequireComponent(typeof(PolygonCollider2D), typeof(Rigidbody2D))]
public class SpaceshipBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected SomeStorageFloat healthPoints;
    [Space]
    [SerializeField] protected bool canMove;
    [SerializeField] protected float moveSpeed;
    
    public event Action OnDead;
    
    protected bool CanShoot;
    protected bool CanMove;
    protected bool IsDead = false;
    
    private void Awake() => OnAwake();
    private void Start() => OnStart();

    protected virtual void OnAwake()
    {
        CanMove = canMove;
    }

    protected virtual void OnStart() { }
    
    public virtual void TakeDamage(float damage)
    {
        healthPoints.ChangeCurrentValue(-damage);

        if (healthPoints.IsEmpty)
        {
            OnDead?.Invoke();
        }
    }
}
