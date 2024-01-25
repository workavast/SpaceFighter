using System;
using System.Collections.Generic;
using Audio;
using TimerExtension;
using UnityEngine;

namespace Projectiles
{
    public class Ray
    {
        private readonly float _damage;
        private readonly List<IDamageable> _damageables = new();
        private readonly Timer _existTimer;
        private readonly SingleAudioSource _singleAudioSource;
        private readonly Transform _transform;
        
        private Transform _parent;

        public event Action OnExistTimerEnd;

        public Ray(float damage, float existTime, SingleAudioSource singleAudioSource, Transform rayTransform)
        {
            _damage = damage;
            _singleAudioSource = singleAudioSource;
            _transform = rayTransform;
            
            _existTimer = new Timer(existTime);
        
            _existTimer.OnTimerEnd += ExistTimerEnd;
            
            ResetTimer();
        }

        private void ExistTimerEnd() => OnExistTimerEnd?.Invoke();
        
        public void SetMount(Transform parentTransform) => _parent = parentTransform;
        
        public void TimerTick(float time) => _existTimer.Tick(time);
    
        public void ResetTimer()
        {
            _existTimer.Reset();
            _existTimer.Continue();
        }

        public void StopTimer() => _existTimer.Stop();
    
        public void Move()
        {
            if (!_parent) return;
        
            _transform.position = _parent.position;
            _transform.rotation = _parent.rotation;
        }

        public void DamagePerSecond(float time)
        {
            foreach (var damageable in _damageables)
                damageable.TakeDamage(_damage * time);
        }
        
        public void OnIDamageableTriggerEnter(IDamageable iDamageable) => _damageables.Add(iDamageable);

        public void OnTriggerExit2D(IDamageable iDamageable) => _damageables.Remove(iDamageable);
        
        public void PlaySound() => _singleAudioSource.Play();
        public void StopSound() => _singleAudioSource.Stop();
    }
}