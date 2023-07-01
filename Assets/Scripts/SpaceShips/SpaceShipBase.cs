using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpaceShipBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected SomeStorage healthPoints;
    [SerializeField] protected SomeStorage levels;
    [SerializeField] protected bool canShoot;
    [SerializeField] protected bool canMove;
    [SerializeField] protected SomeStorage fireRate;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected DictionaryInspector<int, List<Transform>> shootPositions;
    [SerializeField] protected Transform bulletsParent;
    private Pool<BulletBase> _bulletsPool;
    protected Action IsDead;
    private void Awake() => OnAwake();
    private void Start() => OnStart();
    private void Update() => OnUpdate();
    private void FixedUpdate() => OnFixedUpdate();

    protected bool CanShoot;
    protected bool CanMove;
    
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
        
        foreach (var shootPos in shootPositions[(int)levels.CurrentValue])
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
            Debug.Log("dead");
            IsDead?.Invoke();
        }
    }
}
