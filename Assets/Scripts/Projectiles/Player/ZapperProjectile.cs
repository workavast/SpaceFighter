using System;
using System.Collections;
using System.Collections.Generic;
using SomeStorages;
using UnityEngine;

public class ZapperProjectile : PlayerProjectileBase
{
    [SerializeField] private float existTime;
    [SerializeField] private SomeStorageFloat damagePause;
    
    public override PlayerProjectilesEnum PoolId => PlayerProjectilesEnum.Zapper;
    
    protected override bool DestroyableOnCollision => false;
    protected override bool ReturnInPoolOnExitFromPlayArea => false;

    private Transform _follower;

    private void Awake()
    {
        OnElementExtractFromPoolEvent += OnElementExtractFromPoolEventMethod;
        OnElementReturnInPoolEvent += OnElementReturnInPoolMethod;
    }

    protected override void Move()
    {
        if (_follower) transform.position = _follower.position;
    }

    private void OnElementExtractFromPoolEventMethod()
    {
        StartCoroutine(ExistTimer());
    }
    
    private void OnElementReturnInPoolMethod()
    {
        StopCoroutine(ExistTimer());
        _follower = null;
    }
    
    IEnumerator ExistTimer()
    {
        yield return new WaitForSeconds(existTime);
        HandleReturnInPool();
    }

    public void SetMount(Transform transform)
    {
        _follower = transform;
    }
}