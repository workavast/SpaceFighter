using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using PathCreation;
using Unity.VisualScripting;

public class EnemySpaceShip : SpaceShipBase, IPoolable<EnemySpaceShip, SpaceShips>
{

    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private EndOfPathInstruction endOfPathInstruction;
    [SerializeField] private bool fixedRotation;
    [SerializeField] private bool accelerated;
    [SerializeField] private AnimationCurve acceleration;
    [SerializeField] private float contactDamage;
    private float _accelerationTimer = 0;
    private float _distanceTravelled;
    private float _currentMoveSpeed;
    
    
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
        
        // if (_distanceTravelled >= pathCreator.path.length) _distanceTravelled -= pathCreator.path.length;
        if (_distanceTravelled >= pathCreator.path.length) ReturnElementEvent?.Invoke(this);
        
        if (pathCreator)
        {
            _distanceTravelled += _currentMoveSpeed * Time.deltaTime;
            Vector3 prevPosition = transform.position;
            transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled, endOfPathInstruction);

            if(!fixedRotation)
            {
                Quaternion oldRotation = transform.rotation;
                Quaternion newRotation = pathCreator.path.GetRotationAtDistance(_distanceTravelled, endOfPathInstruction);
                if (prevPosition.x < transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(oldRotation.eulerAngles.x, oldRotation.eulerAngles.y, -90-newRotation.eulerAngles.x);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(oldRotation.eulerAngles.x, oldRotation.eulerAngles.y, +90+newRotation.eulerAngles.x);
                }
                // transform.rotation = pathCreator.path.GetRotationAtDistance(_distanceTravelled, endOfPathInstruction);
            }

            if (accelerated)
            {
                _accelerationTimer += Time.deltaTime;
                _currentMoveSpeed += acceleration.Evaluate(_accelerationTimer) * Time.deltaTime;
            }
        }
        
        fireRate.ChangeCurrentValue(Time.deltaTime);
        if (fireRate.IsFull)
        {
            Shoot();
            fireRate.SetCurrentValue(0);
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
        
        healthPoints.SetCurrentValue(healthPoints.MaxValue);
        fireRate.SetCurrentValue(0);
        
        if(fixedRotation) transform.rotation = Quaternion.Euler(0,0,180);
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