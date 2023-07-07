using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using SomeStorages;

public class SpaceShipBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected SomeStorageFloat healthPoints;
    [SerializeField] protected SomeStorageInt levels;
    [SerializeField] protected SomeStorageFloat fireRate;
    [Space]
    [SerializeField] protected bool canShoot;
    [SerializeField] protected bool canMove;
    [SerializeField] protected float moveSpeed;
    [Space]
    [SerializeField] protected GameObject bulletPrefab;
    [Space]
    [SerializeField] protected Transform bulletsParent;
    [SerializeField] protected DictionaryInspector<int, List<Transform>> shootPositions;
    
    private Pool<BulletBase> _bulletsPool;
    protected Action OnDead;
    
    protected bool CanShoot;
    protected bool CanMove;
    protected bool IsDead = false;
    
    public IReadOnlySomeStorage<float> HealthPoints => healthPoints;
    
    private void Awake() => OnAwake();
    private void Start() => OnStart();
    private void Update() => OnUpdate();
    private void FixedUpdate() => OnFixedUpdate();

    protected virtual void OnAwake()
    {
        _bulletsPool = new Pool<BulletBase>(BulletInstantiate);

        CanShoot = canShoot;
        CanMove = canMove;
    }

    protected virtual void OnStart() { }

    protected virtual void OnUpdate() { }

    protected virtual void OnFixedUpdate() { }
    
    protected void Shoot()
    {
        if(!CanShoot) return;
        
        foreach (var shootPos in shootPositions[levels.CurrentValue])
            if (_bulletsPool.ExtractElement(out BulletBase bullet))
            {
                bullet.transform.position = shootPos.position;
                bullet.transform.rotation = shootPos.rotation;
            }
    }

    private BulletBase BulletInstantiate()
    {
        return Instantiate(bulletPrefab, transform.position, transform.rotation, bulletsParent).GetComponent<BulletBase>();
    }

    public virtual void TakeDamage(float damage)
    {
        healthPoints.ChangeCurrentValue(-damage);

        if (healthPoints.IsEmpty)
        {
            OnDead?.Invoke();
        }
    }
}
