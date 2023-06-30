using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using PathCreation;
using Unity.VisualScripting;

enum EnemyRotationType
{
    Forward,
    PlayerTarget,
    PathWayRotation
}

enum PathWayMoveType
{
    Loop,
    OnEndRemove,
    OnEndStop
}
public class EnemySpaceShip : SpaceShipBase, IPoolable<EnemySpaceShip, SpaceShips>
{

    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private EndOfPathInstruction endOfPathInstruction;
    [SerializeField] private bool accelerated;
    [SerializeField] private AnimationCurve acceleration;
    [SerializeField] private float contactDamage;
    [SerializeField] private EnemyRotationType rotationType;
    [SerializeField] private PathWayMoveType moveType;
    private float _accelerationTimer = 0;
    private float _distanceTravelled;
    private float _currentMoveSpeed;
    private Vector3 _prevPosition;
    
    [SerializeField] private SpaceShips poolId;
    public SpaceShips PoolId => poolId;
    public event Action<EnemySpaceShip> ReturnElementEvent;
    public event Action<EnemySpaceShip> DestroyElementEvent;
    

    protected override void OnAwake()
    {
        base.OnAwake();
        _currentMoveSpeed = moveSpeed;
        if (pathCreator)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }

        IsDead += Dead;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        
        if (pathCreator && CanMove) Move();
        if (pathCreator) Rotate();
        
        fireRate.ChangeCurrentValue(Time.deltaTime);
        if (fireRate.IsFull)
        {
            Shoot();
            fireRate.SetCurrentValue(0);
        }
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
                    break;
                
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
                transform.up = PlayerSpaceShip.Instance.transform.position - transform.position;
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
    
    void OnPathChanged() {
        _distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }

    public void ChangePathWay(PathCreator newPathWay)
    {
        pathCreator.pathUpdated -= OnPathChanged;
        
        pathCreator = newPathWay;
        _distanceTravelled = 0;
        pathCreator.pathUpdated += OnPathChanged;
    }

    public void OnElementExtractFromPool()
    {
        gameObject.SetActive(true);

        _accelerationTimer = 0;
        _distanceTravelled = 0;
        _currentMoveSpeed = moveSpeed;
        CanMove = canMove;
        CanShoot = canShoot;
        
        healthPoints.SetCurrentValue(healthPoints.MaxValue);
        fireRate.SetCurrentValue(0);
    }

    public void OnElementReturnInPool()
    {
        gameObject.SetActive(false);
    }

    private void Dead()
    {
        ReturnElementEvent?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        PlayerSpaceShip playerSpaceShip = col.gameObject.GetComponentInChildren<PlayerSpaceShip>();
        if (playerSpaceShip)
        {
            playerSpaceShip.TakeDamage(contactDamage);
        }
    }
}