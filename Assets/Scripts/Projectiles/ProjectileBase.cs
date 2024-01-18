using System;
using PoolSystem;
using UnityEngine;

namespace Projectiles
{
    public abstract class ProjectileBase<TEnum, TScript> : MonoBehaviour, IPoolable<TScript, TEnum>, IPlayAreaCollision, IHandleUpdate
        where TScript : ProjectileBase<TEnum, TScript>
    {
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected float damage;
        [SerializeField] protected Animator animator;
        public abstract TEnum PoolId { get; }
    
        public event Action<TScript> OnLifeTimeEnd;
        public event Action<TScript> OnDestroyElementEvent;
    
        protected abstract bool DestroyableOnCollision { get; }
        protected abstract bool ReturnInPoolOnExitFromPlayArea { get; }
    
        protected event Action OnElementExtractFromPoolEvent;
        protected event Action OnElementReturnInPoolEvent;
        protected event Action<float> OnHandleUpdate;
    
        public void HandleUpdate(float time)
        {
            OnHandleUpdate?.Invoke(time);
            Move(time);
        }

        public void ChangeAnimatorState(bool animatorEnabled) => animator.enabled = animatorEnabled;
        
        public void OnExtractFromPool()
        {
            gameObject.SetActive(true);
            OnElementExtractFromPoolEvent?.Invoke();
        }

        public void OnReturnInPool()
        {
            OnElementReturnInPoolEvent?.Invoke();
            gameObject.SetActive(false);
        }

        public void HandleReturnInPool()
        {
            OnLifeTimeEnd?.Invoke((TScript)this);
        }

        public void EnterInPlayArea() { }

        public void ExitFromPlayerArea()
        {
            if(ReturnInPoolOnExitFromPlayArea) HandleReturnInPool();
        }
    
        protected virtual void Move(float time)
        {
            transform.Translate(Vector3.up * (moveSpeed * time));
        }
        
        private void OnDestroy() => OnDestroyElementEvent?.Invoke((TScript)this);

        private void OnTriggerEnter2D(Collider2D someCollider)
        {
            if (someCollider.gameObject.TryGetComponent(out IDamageable iDamageable))
                OnIDamageableTriggerEnter(iDamageable);
        }

        protected virtual void OnIDamageableTriggerEnter(IDamageable iDamageable)
        {
            iDamageable.TakeDamage(damage);

            if (DestroyableOnCollision) HandleReturnInPool();
        }
    }
}
