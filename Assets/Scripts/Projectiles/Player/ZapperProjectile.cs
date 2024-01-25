using Audio;
using UnityEngine;

namespace Projectiles.Player
{
    [RequireComponent(typeof(SingleAudioSource))]
    public class ZapperProjectile : PlayerProjectileBase
    {
        [SerializeField] private float existTime;
           
        public override PlayerProjectileType PoolId => PlayerProjectileType.Zapper;
    
        protected override bool DestroyableOnCollision => false;
        protected override bool ReturnInPoolOnExitFromPlayArea => false;
        
        private Ray _ray;
        
        public override void Init(bool gameCycleActive)
        {
            base.Init(gameCycleActive);
            
            var singleAudioSource = GetComponent<SingleAudioSource>();
            _ray = new Ray(damage, existTime, singleAudioSource, transform);
            
            _ray.OnExistTimerEnd += HandleReturnInPool;
            OnElementExtractFromPoolEvent += _ray.ResetTimer;
            OnElementReturnInPoolEvent += _ray.StopTimer;

            OnHandleUpdate += _ray.TimerTick;
            OnHandleUpdate += _ray.DamagePerSecond;
            
            OnElementExtractFromPoolEvent += TryPlaySound;
            OnElementReturnInPoolEvent += _ray.StopSound;
            OnGameCycleStateEnter += TryPlaySound;
            OnGameCycleStateExit += _ray.StopSound;
            
            TryPlaySound();
        }

        public void SetMount(Transform parentTransform)
            => _ray.SetMount(parentTransform);
        
        protected override void Move(float time) 
            => _ray.Move();
        
        protected override void OnIDamageableTriggerEnter(IDamageable iDamageable) 
            => _ray.OnIDamageableTriggerEnter(iDamageable);
        

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable iDamageable))
                _ray.OnTriggerExit2D(iDamageable);
        }

        private void TryPlaySound()
        {
            if(ExtractedFromPool && GameCycleActive) _ray.PlaySound();
        }
    }
}