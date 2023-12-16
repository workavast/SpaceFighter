using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(BoxCollider2D))]
public class MoneyStar : MonoBehaviour, PoolSystem.IPoolable<MoneyStar>, IPlayAreaCollision, IHandleUpdate
{
    [SerializeField] private float moveSpeed;
    
    public event Action<MoneyStar> ReturnElementEvent;
    public event Action<MoneyStar> DestroyElementEvent;

    public void HandleUpdate() => Move();

    private void Move()
    {
        transform.Translate(Vector3.down * (moveSpeed * Time.deltaTime));
    }
    
    public void OnExtractFromPool() => gameObject.SetActive(true);

    public void OnReturnInPool() => gameObject.SetActive(false);

    public void EnterInPlayArea() { }

    public void ExitFromPlayerArea() => ReturnElementEvent?.Invoke(this);
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<PlayerSpaceship2>(out PlayerSpaceship2 playerSpaceship))
        {
            LevelMoneyStarsCounter.ChangeValue(1);
            ReturnElementEvent?.Invoke(this);
        }
    }

    private void OnDestroy()
    {
        DestroyElementEvent?.Invoke(this);
    }
}
