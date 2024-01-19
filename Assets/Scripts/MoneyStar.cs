using System;
using SpaceShips;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MoneyStar : MonoBehaviour, PoolSystem.IPoolable<MoneyStar>, IPlayAreaCollision, IHandleUpdate
{
    [SerializeField] private float moveSpeed;
    
    public event Action<MoneyStar> OnStarTaking;
    public event Action<MoneyStar> OnLoseStar;
    public event Action<MoneyStar> OnDestroyElementEvent;

    public void HandleUpdate(float time) => Move(time);

    private void Move(float time)
    {
        transform.Translate(Vector3.down * (moveSpeed * time));
    }
    
    public void OnExtractFromPool() => gameObject.SetActive(true);

    public void OnReturnInPool() => gameObject.SetActive(false);

    public void EnterInPlayArea() { }

    public void ExitFromPlayerArea() => OnLoseStar?.Invoke(this);
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out PlayerSpaceship playerSpaceship))
            OnStarTaking?.Invoke(this);
    }

    private void OnDestroy() => OnDestroyElementEvent?.Invoke(this);
}
