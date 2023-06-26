using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour, IPoolable<BulletBase>
{
    public event Action<BulletBase> ReturnElementEvent;
    public event Action<BulletBase> DestroyElementEvent;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;
        
    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }

    public void OnElementExtractFromPool()
    {
        gameObject.SetActive(true);
    }

    public void OnElementReturnInPool()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out IDamageable iDamageable))
        {
            iDamageable.TakeDamage(damage);
            ReturnElementEvent?.Invoke(this);
        }
    }
}
