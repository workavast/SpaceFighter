using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : MonoBehaviour
{
    [SerializeField] private Transform leftDownPivot;
    public Transform LeftDownPivot => leftDownPivot;

    public static PlayArea Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<BulletBase>(out BulletBase bullet))
        {
            bullet.ReturnInPool();
        }
    }
}