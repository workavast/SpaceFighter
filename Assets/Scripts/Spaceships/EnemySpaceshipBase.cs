using System;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using SomeStorages;
using PoolSystem;

public abstract class EnemySpaceshipBase : SpaceshipBase, IPoolable<EnemySpaceshipBase, EnemySpaceshipsEnum>, IHandleUpdate
{
    [Space]
    [SerializeField] private float collisionDamage = 1;

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

    public abstract EnemySpaceshipsEnum PoolId { get; }
    public event Action<EnemySpaceshipBase> ReturnElementEvent;
    public event Action<EnemySpaceshipBase> DestroyElementEvent;

    protected event Action OnElementExtractFromPoolEvent;
    protected event Action OnElementReturnInPoolEvent;
    protected event Action OnHandleUpdate;
    
    protected override void OnAwake()
    {
        base.OnAwake();

        _moveSpeed = 0;

        OnDead += StartDying;
        _animationControllerEnemy = GetComponentInChildren<AnimationControllerEnemy>();
        _animationControllerEnemy.OnAwake(this);
    }

    public void HandleUpdate()
    {
        if(IsDead) return;
        
        if (_pathCreator && _canMove) Move();
        if (_pathCreator) Rotate();
        
        OnHandleUpdate?.Invoke();
    }

    private void Move()
    {
        if (_accelerated)
        {
            _accelerationTimer += Time.deltaTime;
            _moveSpeed += _acceleration.Evaluate(_accelerationTimer) * Time.deltaTime;
        }
        
        _distanceTravelled += _moveSpeed * Time.deltaTime;
        if (_distanceTravelled >= _pathCreator.path.length)
            switch (_moveType)
            {
                case EnemyPathWayMoveType.Loop:
                    _distanceTravelled -= _pathCreator.path.length;
                    break;
                
                case EnemyPathWayMoveType.OnEndRemove:
                    ReturnElementEvent?.Invoke(this);
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
                transform.rotation = Quaternion.Euler(0,0,180);
                break;
            
            case EnemyRotationType.PlayerTarget:
                transform.up = PlayerSpaceship.Instance.transform.position - transform.position;
                break;
            
            case EnemyRotationType.PathWayRotation:
                Quaternion oldRotation = transform.rotation;
                Quaternion newRotation = _pathCreator.path.GetRotationAtDistance(_distanceTravelled, _endOfPathInstruction);
                if (_prevPosition.x < transform.position.x)
                    transform.rotation = Quaternion.Euler(oldRotation.eulerAngles.x, oldRotation.eulerAngles.y, -90-newRotation.eulerAngles.x);
                else
                    transform.rotation = Quaternion.Euler(oldRotation.eulerAngles.x, oldRotation.eulerAngles.y, +90+newRotation.eulerAngles.x);
                break;
            
            default: throw new Exception("Undeclared rotationType (enum EnemyRotationType) type");
        }
    }
    
    void OnPathUpdate() {
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
        
        OnElementExtractFromPoolEvent?.Invoke();
    }

    public void OnReturnInPool()
    {
        OnElementReturnInPoolEvent?.Invoke();
        
        gameObject.SetActive(false);
    }

    private void StartDying()
    {
        if (IsDead) return;

        IsDead = true;
        _animationControllerEnemy.SetDyingTrigger();

        MoneyStarsSpawner.Spawn(transform);
    }

    public void EndDying()
    {
        ReturnElementEvent?.Invoke(this);
    }

    private void OnDestroy()
    {
        DestroyElementEvent?.Invoke(this);
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