using System;
using System.Collections.Generic;
using UnityEngine;
using SomeStorages;

public class PlayerSpaceShip : SpaceShipBase
{
    private enum DamageAnimatorTriggerName
    {
        NoDamage,
        SlightDamage,
        AverageDamage,
        SignificantDamage
    }
    
    [Serializable]
    private struct DamageAnimatorTriggerData
    {
        public DamageAnimatorTriggerName animatorTriggerName;
        
        [Tooltip("Percent of health points at which animatorTriggerName apply in animator (0 -> 0%; 1 -> 100%)")] 
        [Range(0,1)] public float healthPointsPercent;
    }
    
    [Space]
    [SerializeField] private Animator spaceshipModelAnimator;
    [SerializeField] private List<DamageAnimatorTriggerData> damageAnimatorTriggerData;

    public static PlayerSpaceShip Instance { get; private set; }
   
    private Transform _playAreaLeftDownPivot;
    private Camera _camera;
    
    private SomeStorageInt _currentDamageSprite;

    protected override void OnAwake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }
        
        base.OnAwake();
        
        Instance = this;
        _camera = Camera.main;

        if (!spaceshipModelAnimator)
        {
            spaceshipModelAnimator = GetComponentInChildren<Animator>();
            Debug.LogWarning("You dont Serialize spaceshipModelAnimator");
        }

        _currentDamageSprite = new SomeStorageInt(damageAnimatorTriggerData.Count - 1);
    }

    protected override void OnStart()
    {
        base.OnStart();
        _playAreaLeftDownPivot = PlayArea.Instance.LeftDownPivot;
    }
    
    protected override void OnUpdate()
    {
        base.OnUpdate();
        
        Move();
        
        fireRate.ChangeCurrentValue(Time.deltaTime);
        if (fireRate.IsFull)
        {
            Shoot();
            fireRate.SetCurrentValue(0);
        }
    }
    
    protected void Move()
    {
        Vector3 targetPosition = _camera.ScreenToWorldPoint(Input.mousePosition) - _camera.transform.position;
        Vector3 playAreaPivotPosition = _playAreaLeftDownPivot.position;

        float x = Mathf.Clamp(targetPosition.x, playAreaPivotPosition.x, -playAreaPivotPosition.x);
        float y = Mathf.Clamp(targetPosition.y, playAreaPivotPosition.y, -playAreaPivotPosition.y);

        targetPosition = new Vector3(x, y, targetPosition.z);
        
        float finalSpeed = Mathf.Clamp(Vector3.Distance(transform.position, targetPosition), 0, moveSpeed * Time.deltaTime);

        transform.Translate((targetPosition - transform.position).normalized * finalSpeed);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (_currentDamageSprite.CurrentValue >= 0 && _currentDamageSprite.CurrentValue < damageAnimatorTriggerData.Count)
        {
            if (!_currentDamageSprite.IsFull && damageAnimatorTriggerData[_currentDamageSprite.CurrentValue + 1].healthPointsPercent > healthPoints.FillingPercentage)
            {
                _currentDamageSprite.ChangeCurrentValue(1);
                spaceshipModelAnimator.SetTrigger(damageAnimatorTriggerData[_currentDamageSprite.CurrentValue].animatorTriggerName.ToString());
            }
            else
            {
                if (_currentDamageSprite.CurrentValue > 0 && damageAnimatorTriggerData[_currentDamageSprite.CurrentValue].healthPointsPercent <= healthPoints.FillingPercentage)
                {
                    _currentDamageSprite.ChangeCurrentValue(-1);
                    spaceshipModelAnimator.SetTrigger(damageAnimatorTriggerData[_currentDamageSprite.CurrentValue].animatorTriggerName.ToString());
                }
            }
        }
    }
}