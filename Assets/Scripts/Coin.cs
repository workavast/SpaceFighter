using System;
using SpaceShips;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Coin : MonoBehaviour, PoolSystem.IPoolable<Coin>, IPlayAreaCollision, IHandleUpdate
{
    [SerializeField] private float moveSpeed;
    
    public event Action<Coin> OnPickUp;
    public event Action<Coin> OnLose;
    public event Action<Coin> OnDestroyElementEvent;

    public void HandleUpdate(float time) => Move(time);

    private void Move(float time)
    {
        transform.Translate(Vector3.down * (moveSpeed * time));
    }
    
    public void OnExtractFromPool() => gameObject.SetActive(true);

    public void OnReturnInPool() => gameObject.SetActive(false);

    public void EnterInPlayArea() { }

    public void ExitFromPlayerArea() => OnLose?.Invoke(this);
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out PlayerSpaceship playerSpaceship))
            OnPickUp?.Invoke(this);
    }

    private void OnDestroy() => OnDestroyElementEvent?.Invoke(this);
}
