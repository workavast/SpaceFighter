using System;
using SpaceShips;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Coin : MonoBehaviour, PoolSystem.IPoolable<Coin>, IPlayAreaCollision, IHandleUpdate
{
    [SerializeField] private float moveSpeed;
    
    private bool _extractedFromPool;

    public event Action OnPickUp;
    public event Action<Coin> ReturnElementEvent;
    public event Action<Coin> DestroyElementEvent;
    
    public void HandleUpdate(float time) 
        => Move(time);

    private void Move(float time)
        => transform.Translate(Vector3.down * (moveSpeed * time));
    
    public void OnElementExtractFromPool()
    {
        _extractedFromPool = true;
        gameObject.SetActive(true);
    }

    public void OnElementReturnInPool()
    {
        _extractedFromPool = false;
        gameObject.SetActive(false);
    }

    public void EnterInPlayArea() { }

    public void ExitFromPlayerArea()
        => ReturnInPool();
    
    private void ReturnInPool()
    {
        if(_extractedFromPool)
            ReturnElementEvent?.Invoke(this);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out PlayerSpaceship playerSpaceship))
        {
            OnPickUp?.Invoke();
            ReturnInPool();
        }
    }
    
    private void OnDestroy() 
        => DestroyElementEvent?.Invoke(this);
}
