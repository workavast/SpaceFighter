using TimerExtension;
using UnityEngine;

public class ZapperProjectile : PlayerProjectileBase
{
    public override PlayerProjectilesEnum PoolId => PlayerProjectilesEnum.Zapper;
    
    [SerializeField] private float existTime;
    [SerializeField] private float damagePause;//dont used at the moment, maybe used damage per second??
    
    protected override bool DestroyableOnCollision => false;
    protected override bool ReturnInPoolOnExitFromPlayArea => false;
    
    private Transform _follower;

    private Timer _existTimer;
    private Timer _damagePause;//dont used at the moment, maybe used damage per second??(but need override OnTargetEnter)
    
    private void Awake()
    {
        _existTimer = new Timer(existTime);
        _damagePause = new Timer(damagePause);
        
        _existTimer.OnTimerEnd += HandleReturnInPool;
        OnElementExtractFromPoolEvent += ResetTimers;
        OnElementReturnInPoolEvent += StopTimers;

        OnHandleUpdate += _existTimer.Tick;
        OnHandleUpdate += _damagePause.Tick;
        
        ResetTimers();
    }

    private void ResetTimers()
    {
        _existTimer.Reset();
        _damagePause.Reset();
        
        _existTimer.Continue();
        _damagePause.Continue();
    }

    private void StopTimers()
    {
        _existTimer.Stop();
        _damagePause.Stop();
    }
    
    protected override void Move(float time)
    {
        if (_follower) transform.position = _follower.position;
    }

    public void SetMount(Transform transform)
    {
        _follower = transform;
    }
}