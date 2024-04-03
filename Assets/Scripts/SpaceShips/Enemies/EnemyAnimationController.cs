using UnityEngine;

namespace SpaceShips.Enemies
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimationController : MonoBehaviour
    {
        private static readonly int DyingStringHash = Animator.StringToHash("Dying");
        private static readonly int AliveStringHash = Animator.StringToHash("Alive");
        
        private EnemySpaceshipBase _enemySpaceshipBase;
        private Animator _animator;

        public void OnAwake(EnemySpaceshipBase enemySpaceshipBase)
        {
            _enemySpaceshipBase = enemySpaceshipBase;
            _animator = GetComponent<Animator>();
        }

        public void OnDyingEnd()
        {
            SetAliveTrigger();
            _enemySpaceshipBase.EndDying();
        }
    
        public void ChangeAnimatorState(bool animatorEnabled) 
            => _animator.enabled = animatorEnabled;
        
        public void SetDyingTrigger()
            => _animator.SetTrigger(DyingStringHash);
    
        private void SetAliveTrigger()
            => _animator.SetTrigger(AliveStringHash);
    }
}
