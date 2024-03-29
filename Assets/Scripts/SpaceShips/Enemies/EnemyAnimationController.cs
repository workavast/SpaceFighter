using UnityEngine;

namespace SpaceShips.Enemies
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimationController : MonoBehaviour
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
    
        public void ChangeAnimatorState(bool animatorEnabled) => _animator.enabled = animatorEnabled;
    }
}
