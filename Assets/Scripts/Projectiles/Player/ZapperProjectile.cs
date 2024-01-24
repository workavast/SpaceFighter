using System.Collections.Generic;
using Audio;
using TimerExtension;
using UnityEngine;

namespace Projectiles.Player
{
    [RequireComponent(typeof(SingleAudioSource))]
    public class ZapperProjectile : PlayerProjectileBase
    {
        public override PlayerProjectileType PoolId => PlayerProjectileType.Zapper;
    
        [SerializeField] private float existTime;
           
        private readonly List<IDamageable> _damageables = new();

        protected override bool DestroyableOnCollision => false;
        protected override bool ReturnInPoolOnExitFromPlayArea => false;

        private Transform _parent;
        private Timer _existTimer;
        private SingleAudioSource _singleAudioSource;

        public override void Init(bool gameCycleActive)
        {
            base.Init(gameCycleActive);
            
            _singleAudioSource = GetComponent<SingleAudioSource>();
            
            _existTimer = new Timer(existTime);
        
            _existTimer.OnTimerEnd += HandleReturnInPool;
            OnElementExtractFromPoolEvent += ResetTimer;
            OnElementReturnInPoolEvent += StopTimer;

            OnHandleUpdate += TimerTick;
            OnHandleUpdate += DamagePerSecond;
            
            OnElementExtractFromPoolEvent += TryPlaySound;
            OnElementReturnInPoolEvent += StopSound;
            OnGameCycleStateEnter += TryPlaySound;
            OnGameCycleStateExit += StopSound;
            
            TryPlaySound();
            
            ResetTimer();
        }

        public void SetMount(Transform parentTransform) => _parent = parentTransform;
        
        private void TimerTick(float time)
        {
            _existTimer.Tick(time);
        }
    
        private void ResetTimer()
        {
            _existTimer.Reset();
        
            _existTimer.Continue();
        }

        private void StopTimer()
        {
            _existTimer.Stop();
        }
    
        protected override void Move(float time)
        {
            if (!_parent) return;
        
            transform.position = _parent.position;
            transform.rotation = _parent.rotation;
        }

        private void DamagePerSecond(float time)
        {
            foreach (var damageable in _damageables)
                damageable.TakeDamage(damage * time);
        }
        
        protected override void OnIDamageableTriggerEnter(IDamageable iDamageable)
        {
            _damageables.Add(iDamageable);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable iDamageable))
                _damageables.Remove(iDamageable);
        }

        private void TryPlaySound()
        {
            if(ExtractedFromPool && GameCycleActive) PlaySound();
        }
        
        private void PlaySound() => _singleAudioSource.Play();
        private void StopSound() => _singleAudioSource.Stop();
    }
}