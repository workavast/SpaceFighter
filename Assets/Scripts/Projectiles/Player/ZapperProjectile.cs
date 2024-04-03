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

            OnSetData += InitRay;
        }

        private void InitRay()
        {
            var singleAudioSource = GetComponent<SingleAudioSource>();
            _ray = new Ray(damage, existTime, singleAudioSource, transform);
            _ray.OnExistTimerEnd += ReturnInPool;
            
            OnHandleUpdate += _ray.TimerTick;
            OnHandleUpdate += _ray.DamagePerSecond;
            
            OnGameCycleStateExit += _ray.StopSound;
            OnGameCycleStateEnter += TryPlaySound;
            
            TryPlaySound();
        }

        public override void OnElementExtractFromPool()
        {
            base.OnElementExtractFromPool();
            
            _ray.ResetTimer();
            TryPlaySound();
        }

        public override void OnElementReturnInPool()
        {
            base.OnElementReturnInPool();
            
            _ray.StopTimer();
            _ray.StopSound();
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