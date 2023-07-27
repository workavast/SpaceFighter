using System;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using SomeStorages;

public abstract class EnemySpaceshipBase : SpaceshipBase, IPoolable<EnemySpaceshipBase, EnemySpaceshipsEnum>, IHandleUpdate
{
    private enum EnemyRotationType
    {
        Forward,
        PlayerTarget,
        PathWayRotation
    }

    private enum PathWayMoveType
    {
        Loop,
        OnEndRemove,
        OnEndStop
    }
    
    [Space]
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private EndOfPathInstruction endOfPathInstruction;
    [SerializeField] private PathWayMoveType moveType;
    [SerializeField] private EnemyRotationType rotationType;
    [Space]
    [SerializeField] private bool accelerated;
    [SerializeField] private AnimationCurve acceleration;
    [Space]
    [SerializeField] private float collisionDamage = 1;

    private AnimationControllerEnemy _animationControllerEnemy;
    
    private float _accelerationTimer = 0;
    private float _distanceTravelled;
    private float _currentMoveSpeed;
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

        _currentMoveSpeed = moveSpeed;
        if (pathCreator)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathUpdate;
        }
        else
        {
            Debug.LogWarning("pathCreator is null");
        }

        OnDead += StartDying;
        _animationControllerEnemy = GetComponentInChildren<AnimationControllerEnemy>();
        _animationControllerEnemy.OnAwake(this);
    }

    public void HandleUpdate()
    {
        if(IsDead) return;
        
        if (pathCreator && CanMove) Move();
        if (pathCreator) Rotate();
        
        OnHandleUpdate?.Invoke();
    }

    private void Move()
    {
        if (accelerated)
        {
            _accelerationTimer += Time.deltaTime;
            _currentMoveSpeed += acceleration.Evaluate(_accelerationTimer) * Time.deltaTime;
        }
        
        _distanceTravelled += _currentMoveSpeed * Time.deltaTime;
        if (_distanceTravelled >= pathCreator.path.length)
            switch (moveType)
            {
                case PathWayMoveType.Loop:
                    _distanceTravelled -= pathCreator.path.length;
                    break;
                
                case PathWayMoveType.OnEndRemove:
                    ReturnElementEvent?.Invoke(this);
                    return;
                
                case PathWayMoveType.OnEndStop:
                    _distanceTravelled = pathCreator.path.length;
                    canMove = false;
                    _accelerationTimer = 0;
                    _currentMoveSpeed = moveSpeed;
                    break;
                
                default: throw new Exception("Undeclared moveType (enum PathWayMoveType) type");
            }
        
        _prevPosition = transform.position;
        transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled, endOfPathInstruction);
    }

    private void Rotate()
    {
        switch (rotationType)
        {
            case EnemyRotationType.Forward:
                transform.rotation = Quaternion.Euler(0,0,180);
                break;
            
            case EnemyRotationType.PlayerTarget:
                transform.up = PlayerSpaceship.Instance.transform.position - transform.position;
                break;
            
            case EnemyRotationType.PathWayRotation:
                Quaternion oldRotation = transform.rotation;
                Quaternion newRotation = pathCreator.path.GetRotationAtDistance(_distanceTravelled, endOfPathInstruction);
                if (_prevPosition.x < transform.position.x)
                    transform.rotation = Quaternion.Euler(oldRotation.eulerAngles.x, oldRotation.eulerAngles.y, -90-newRotation.eulerAngles.x);
                else
                    transform.rotation = Quaternion.Euler(oldRotation.eulerAngles.x, oldRotation.eulerAngles.y, +90+newRotation.eulerAngles.x);
                break;
            
            default: throw new Exception("Undeclared rotationType (enum EnemyRotationType) type");
        }
    }
    
    void OnPathUpdate() {
        _distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }

    public void ChangePathWay(PathCreator newPathWay)
    {
        pathCreator.pathUpdated -= OnPathUpdate;
        
        pathCreator = newPathWay;
        _distanceTravelled = 0;
        pathCreator.pathUpdated += OnPathUpdate;
    }

    public virtual void OnElementExtractFromPool()
    {
        IsDead = false;
        
        _accelerationTimer = 0;
        _distanceTravelled = 0;
        _currentMoveSpeed = moveSpeed;
        CanMove = canMove;
        
        transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled, endOfPathInstruction);

        healthPoints.SetCurrentValue(healthPoints.MaxValue);
        
        gameObject.SetActive(true);
        
        OnElementExtractFromPoolEvent?.Invoke();
    }

    public void OnElementReturnInPool()
    {
        OnElementReturnInPoolEvent?.Invoke();
        
        gameObject.SetActive(false);
    }

    private void StartDying()
    {
        if (IsDead) return;

        IsDead = true;
        _animationControllerEnemy.SetDyingTrigger();
    }

    public void EndDying()
    {
        ReturnElementEvent?.Invoke(this);
    }

    private void OnDestroy()
    {
        DestroyElementEvent?.Invoke(this);
    }

    public void SetMoveSpeed(float newMoveSpeed)
    {
        _currentMoveSpeed = newMoveSpeed;
    }
}