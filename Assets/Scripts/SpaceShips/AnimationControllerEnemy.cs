using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationControllerEnemy : MonoBehaviour
{
    private EnemySpaceShip _enemySpaceShip;
    private Animator _animator;
    
    public void OnAwake(EnemySpaceShip enemySpaceShip)
    {
        _enemySpaceShip = enemySpaceShip;
        _animator = GetComponent<Animator>();
    }

    public void SetDyingTrigger()
    {
        _animator.SetTrigger("Dying");
    }
    
    private void SetAliveTrigger()
    {
        _animator.SetTrigger("Alive");
    }

    public void OnDyingEnd()
    {
        SetAliveTrigger();
        _enemySpaceShip.EndDying();
    }
}
