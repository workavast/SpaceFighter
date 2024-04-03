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
        
        protected bool ExtractedFromPool;
        protected bool GameCycleActive;
        
        protected abstract bool DestroyableOnCollision { get; }
        protected abstract bool ReturnInPoolOnExitFromPlayArea { get; }
        
        protected event Action<float> OnHandleUpdate;
        protected event Action OnGameCycleStateEnter;
        protected event Action OnGameCycleStateExit;

        public event Action<TScript> ReturnElementEvent;
        public event Action<TScript> DestroyElementEvent;

        public virtual void Init(bool gameCycleActive)
        {
            GameCycleActive = gameCycleActive;
        }
        
        public void HandleUpdate(float time)
        {
            OnHandleUpdate?.Invoke(time);
            Move(time);
        }

        public void GameCycleStateEnter()
        {
            GameCycleActive = true;
            ChangeAnimatorState(true);
            OnGameCycleStateEnter?.Invoke();
        }
        
        public void GameCycleStateExit()
        {
            GameCycleActive = false;
            ChangeAnimatorState(false);
            OnGameCycleStateExit?.Invoke();
        }
        
        private void ChangeAnimatorState(bool animatorEnabled) 
            => animator.enabled = animatorEnabled;

        protected void ReturnInPool()
        {
            if(ExtractedFromPool)
                ReturnElementEvent?.Invoke((TScript)this);
        }

        public void EnterInPlayArea() { }

        public void ExitFromPlayerArea()
        {
            if(ReturnInPoolOnExitFromPlayArea) 
                ReturnInPool();
        }
    
        protected virtual void Move(float time)
        {
            transform.Translate(Vector3.up * (moveSpeed * time));
        }
        
        private void OnDestroy() 
            => DestroyElementEvent?.Invoke((TScript)this);

        private void OnTriggerEnter2D(Collider2D someCollider)
        {
            if (someCollider.gameObject.TryGetComponent(out IDamageable iDamageable))
                OnIDamageableTriggerEnter(iDamageable);
        }

        protected virtual void OnIDamageableTriggerEnter(IDamageable iDamageable)
        {
            iDamageable.TakeDamage(damage);

            if (DestroyableOnCollision)
                ReturnInPool();
        }
        
        public virtual void OnElementExtractFromPool()
        {
            ExtractedFromPool = true;
            gameObject.SetActive(true);
        }

        public virtual void OnElementReturnInPool()
        {
            ExtractedFromPool = false;
            gameObject.SetActive(false);
        }
    }
}
