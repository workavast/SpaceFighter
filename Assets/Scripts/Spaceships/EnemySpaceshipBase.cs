using System;
using EventBus;
using EventBus.Events;
using Managers;
using UnityEngine;
using PathCreation;
using Zenject;

public abstract class EnemySpaceshipBase : SpaceshipBase, PoolSystem.IPoolable<EnemySpaceshipBase, EnemySpaceshipType>
{
    [Space]
    [SerializeField] private float collisionDamage = 1;

    [Inject] protected MissionEventBus EventBus;
    [Inject] private PlayerSpaceshipManager _playerSpaceshipManager;
    
    private bool _canMove;
    
    private PathCreator _pathCreator;
    private EndOfPathInstruction _endOfPathInstruction;
    private EnemyPathWayMoveType _moveType;
    private EnemyRotationType _rotationType;
    private bool _accelerated;
    private AnimationCurve _acceleration;

    private AnimationControllerEnemy _animationControllerEnemy;
    
    private float _accelerationTimer = 0;
    private float _distanceTravelled;
    private float _moveSpeed;
    private Vector3 _prevPosition;
    public float CollisionDamage => collisionDamage;

    public abstract EnemySpaceshipType PoolId { get; }
    public event Action<EnemySpaceshipBase> OnDestroyElementEvent;

    protected event Action OnElementExtractFromPool;
    protected event Action OnHandleUpdate;
    
    public event Action<EnemySpaceshipBase> OnGone; 
    
    protected override void OnAwake()
    {
        base.OnAwake();

        _moveSpeed = 0;

        OnDead += StartDying;
        _animationControllerEnemy = GetComponentInChildren<AnimationControllerEnemy>();
        _animationControllerEnemy.OnAwake(this);
    }

    public override void HandleUpdate(float time)
    {
        if(IsDead) return;
        
        if (_pathCreator && _canMove) Move(time);
        if (_pathCreator) Rotate();
        
        OnHandleUpdate?.Invoke();
    }

    public override void ChangeAnimatorState(bool animatorEnabled) =>
        _animationControllerEnemy.ChangeAnimatorState(animatorEnabled);

    private void Move(float time)
    {
        if (_accelerated)
        {
            _accelerationTimer += time;
            _moveSpeed += _acceleration.Evaluate(_accelerationTimer) * time;
        }
        
        _distanceTravelled += _moveSpeed * time;
        if (_distanceTravelled >= _pathCreator.path.length)
            switch (_moveType)
            {
                case EnemyPathWayMoveType.Loop:
                    _distanceTravelled -= _pathCreator.path.length;
                    break;
                case EnemyPathWayMoveType.OnEndRemove:
                    OnGone?.Invoke(this);
                    return;
                case EnemyPathWayMoveType.OnEndStop:
                    _distanceTravelled = _pathCreator.path.length;
                    _canMove = false;
                    _accelerationTimer = 0;
                    _moveSpeed = 0;
                    break;
                
                default: throw new Exception("Undeclared moveType (enum PathWayMoveType) type");
            }
        
        _prevPosition = transform.position;
        transform.position = _pathCreator.path.GetPointAtDistance(_distanceTravelled, _endOfPathInstruction);
    }

    private void Rotate()
    {
        switch (_rotationType)
        {
            case EnemyRotationType.Forward:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            
            case EnemyRotationType.PlayerTarget:
                transform.up = _playerSpaceshipManager.PlayerSpaceship.transform.position - transform.position;
                break;
            
            case EnemyRotationType.PathWayRotation:
                Quaternion oldRotation = transform.rotation;
                Quaternion newRotation = _pathCreator.path.GetRotationAtDistance(_distanceTravelled, _endOfPathInstruction);
                if (_prevPosition.x < transform.position.x)
                    transform.rotation = Quaternion.Euler(oldRotation.eulerAngles.x, oldRotation.eulerAngles.y, -90 - newRotation.eulerAngles.x);
                else
                    transform.rotation = Quaternion.Euler(oldRotation.eulerAngles.x, oldRotation.eulerAngles.y, +90 + newRotation.eulerAngles.x);
                break;
            
            default: throw new Exception("Undeclared rotationType (enum EnemyRotationType) type");
        }
    }
    
    void OnPathUpdate() 
    {
        _distanceTravelled = _pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
    
    public virtual void OnExtractFromPool()
    {
        IsDead = false;
        
        _accelerationTimer = 0;
        _distanceTravelled = 0;
        _moveSpeed = 0;
        _canMove = true;
        
        healthPoints.SetCurrentValue(healthPoints.MaxValue);
        
        gameObject.SetActive(true);
        
        OnElementExtractFromPool?.Invoke();
    }

    public void OnReturnInPool()
    {
        gameObject.SetActive(false);
    }

    private void StartDying()
    {
        if (IsDead) return;

        IsDead = true;
        EventBus.Invoke(new EnemyStartDie(transform.position));
        _animationControllerEnemy.SetDyingTrigger();
    }
    
    public void EndDying() => OnGone?.Invoke(this);

    private void OnDestroy()
    {
        OnDestroyElementEvent?.Invoke(this);
    }
    
    private void ChangePathWay(PathCreator newPathWay)
    {
        if(_pathCreator) _pathCreator.pathUpdated -= OnPathUpdate;
        
        _pathCreator = newPathWay;
        _distanceTravelled = 0;
        _pathCreator.pathUpdated += OnPathUpdate;
    }
    
    public void SetWaveData(float newMoveSpeed, PathCreator newPath, EndOfPathInstruction newEndOfPathInstruction, 
        EnemyPathWayMoveType newEnemyPathWayMoveType, EnemyRotationType newEnemyRotationType, bool newAccelerated, 
        AnimationCurve newAcceleration)
    {
        _moveSpeed = newMoveSpeed;
        ChangePathWay(newPath);
        _endOfPathInstruction = newEndOfPathInstruction;
        _moveType = newEnemyPathWayMoveType;
        _rotationType = newEnemyRotationType;
        _accelerated = newAccelerated;
        _acceleration = newAcceleration;
        
        transform.position = _pathCreator.path.GetPointAtDistance(0, _endOfPathInstruction);
    }
}