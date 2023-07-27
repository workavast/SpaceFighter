using System.Collections;
using System.Collections.Generic;
using SomeStorages;
using UnityEngine;

public class EnemyRay : EnemyProjectileBase
{
    public override EnemyProjectilesEnum PoolId => EnemyProjectilesEnum.Ray;
    
    protected override bool DestroyableOnCollision => false;
    protected override bool ReturnInPoolOnExitFromPlayArea => false;
    
    [SerializeField] private float existTime;
    [SerializeField] private SomeStorageFloat damagePause;

    private Transform _follower;
    
    private void Awake()
    {
        OnElementExtractFromPoolEvent += StartExistTimer;
        OnElementReturnInPoolEvent += CancelExistTimer;
    }

    protected override void Move()
    {
        if (_follower)
        {
            transform.position = _follower.position;
            transform.rotation = _follower.rotation;
        }
    }

    private void CancelExistTimer()
    {
        StopCoroutine(ExistTimer());
    }
    
    private void StartExistTimer()
    {
        StartCoroutine(ExistTimer());
    }

    IEnumerator ExistTimer()
    {
        yield return new WaitForSeconds(existTime);
        ReturnInPool();
    }

    public void SetMount(Transform transform)
    {
        _follower = transform;
    }
}
