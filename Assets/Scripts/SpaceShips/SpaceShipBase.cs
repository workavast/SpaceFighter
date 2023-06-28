using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpaceShipBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected SomeStorage healthPoints;
    [SerializeField] protected SomeStorage levels;
    [SerializeField] protected SomeStorage fireRate;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected DictionaryInspector<int, List<Transform>> shootPositions;
    [SerializeField] protected Transform bulletsParent;
    private Pool<BulletBase> _bulletsPool;

    private void Awake() => OnAwake();
    private void Start() => OnStart();
    private void Update() => OnUpdate();
    private void FixedUpdate() => OnFixedUpdate();
    
    protected virtual void OnAwake()
    {
        _bulletsPool = new Pool<BulletBase>(BulletInstantiate);
    }

    protected virtual void OnStart() { }

    protected virtual void OnUpdate() { }

    protected virtual void OnFixedUpdate() { }
    
    protected void Move(Vector3 targetPosition)
    {
        float finalSpeed = Mathf.Clamp(Vector3.Distance(transform.position, targetPosition), 0, moveSpeed * Time.deltaTime);

        transform.Translate((targetPosition - transform.position).normalized * finalSpeed);
    }

    protected void Shoot()
    {
        foreach (var shootPos in shootPositions[(int)levels.CurrentValue])
            if (_bulletsPool.ExtractElement(out BulletBase bullet))
                bullet.transform.position = shootPos.position;
    }

    private BulletBase BulletInstantiate()
    {
        return Instantiate(bulletPrefab, transform.position, transform.rotation, bulletsParent).GetComponent<BulletBase>();
    }

    public void TakeDamage(float damage)
    {
        healthPoints.ChangeCurrentValue(-damage);

        if (healthPoints.IsEmpty)
        {
            Debug.Log("dead");
        }
    }
}
