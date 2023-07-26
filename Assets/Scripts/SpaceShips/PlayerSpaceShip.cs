using System;
using System.Collections.Generic;
using UnityEngine;
using SomeStorages;
using UnityEditor;
using Zenject;

public class PlayerSpaceship : SpaceshipBase
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
    [SerializeField] private Transform weaponPosition;

    public IReadOnlySomeStorage<float> HealthPoints => healthPoints;

    public static PlayerSpaceship Instance { get; private set; }
   
    private Transform _playAreaLeftDownPivot;
    private Camera _camera;
    
    private SomeStorageInt _currentDamageSprite;
    private PlayerWeaponBase _weapon;
    
    [Inject] private PlayerGlobalData _playerGlobalData;
    [Inject] private PlayerWeaponConfig _playerWeaponConfig;
    [Inject] private DiContainer _diContainer;

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

        SpawnWeapon();
        OnDead += StopShooting;
    }

    private void SpawnWeapon()
    {
        if (_playerWeaponConfig.WeaponsPrefabsData.TryGetValue(_playerGlobalData.CurrentSelectedPlayerWeapons,
                out GameObject prefab))
        {
            GameObject weapon = _diContainer.InstantiatePrefab(prefab, weaponPosition);
            _weapon = weapon.GetComponent<PlayerWeaponBase>();
        }
        else
            throw new Exception("Dictionary don't contain this WeaponsEnum");
    }

    private void StopShooting()
    {
        _weapon.StopShoot();
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        _playAreaLeftDownPivot = PlayArea.Instance.LeftDownPivot;
    }
    
    private void Update()
    {
        if(canMove) Move();
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
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        EnemySpaceshipBase enemySpaceshipBase = col.gameObject.GetComponentInChildren<EnemySpaceshipBase>();
        if (enemySpaceshipBase)
        {
            TakeDamage(enemySpaceshipBase.CollisionDamage);
            enemySpaceshipBase.TakeDamage(float.MaxValue);
        }
    }
}