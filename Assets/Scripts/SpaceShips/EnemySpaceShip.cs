using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using PathCreation;
using Unity.VisualScripting;

public class EnemySpaceShip : SpaceShipBase
{
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private EndOfPathInstruction endOfPathInstruction;
    [SerializeField] private bool fixedRotation;
    [SerializeField] private bool accelerated;
    [SerializeField] private AnimationCurve acceleration;
    private float _accelerationTimer = 0;
    private float _distanceTravelled;
    private float _currentMoveSpeed;
    
    protected override void OnAwake()
    {
        base.OnAwake();
        _currentMoveSpeed = moveSpeed;
        if (pathCreator)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        
        if (_distanceTravelled >= pathCreator.path.length) _distanceTravelled -= pathCreator.path.length;
        
        if (pathCreator)
        {
            _distanceTravelled += _currentMoveSpeed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled, endOfPathInstruction);
            if(!fixedRotation) transform.rotation = pathCreator.path.GetRotationAtDistance(_distanceTravelled, endOfPathInstruction);
            
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
}