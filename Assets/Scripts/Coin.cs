using System;
using Configs;
using SpaceShips;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(BoxCollider2D))]
public class Coin : MonoBehaviour, PoolSystem.IPoolable<Coin>, IPlayAreaCollision, IHandleUpdate
{
    [Inject] private CoinConfig _config;
    
    private float _moveSpeed;
    private bool _extractedFromPool;
    private float _starsCountScale;
    
    /// <summary>
    /// return stars count scale
    /// </summary>
    public event Action<float> OnPickUp;
    public event Action<Coin> ReturnElementEvent;
    public event Action<Coin> DestroyElementEvent;

    private void Awake()
    {
        _moveSpeed = _config.MoveSpeed;
    }

    public void HandleUpdate(float time) 
        => Move(time);

    public void SetStarsCountScale(float newStarsCountScale)
        => _starsCountScale = newStarsCountScale;
    
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
    
    private void Move(float time)
        => transform.Translate(Vector3.down * (_moveSpeed * time));
    
    private void ReturnInPool()
    {
        if(_extractedFromPool)
            ReturnElementEvent?.Invoke(this);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out PlayerSpaceship playerSpaceship))
        {
            OnPickUp?.Invoke(_starsCountScale);
            ReturnInPool();
        }
    }
    
    private void OnDestroy() 
        => DestroyElementEvent?.Invoke(this);
}
