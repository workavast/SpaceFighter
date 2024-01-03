using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationControllerEnemy : MonoBehaviour
{
    private EnemySpaceshipBase _enemySpaceshipBase;
    private Animator _animator;
    
    public void OnAwake(EnemySpaceshipBase enemySpaceshipBase)
    {
        _enemySpaceshipBase = enemySpaceshipBase;
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
        _enemySpaceshipBase.EndDying();
    }
}
