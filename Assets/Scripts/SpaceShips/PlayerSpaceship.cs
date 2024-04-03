using System;
using System.Collections.Generic;
using Configs;
using EventBusEvents;
using EventBusExtension;
using Settings;
using SomeStorages;
using SpaceShips.Enemies;
using UnityEngine;
using Zenject;

namespace SpaceShips
{
    public class PlayerSpaceship : SpaceshipBase
    {
        [Space] 
        [SerializeField] private Animator animator;
        [SerializeField] private List<DamageAnimatorTriggerData> damageAnimatorTriggerData;
        [SerializeField] private Transform weaponPosition;
        [SerializeField] protected bool canMove;
        [SerializeField] protected float moveSpeed;
        [SerializeField] private Animator[] stoppableAnimators;
    
        [Inject] private readonly PlayArea _playArea;
        [Inject] private readonly EventBus _eventBus;

        public Transform WeaponPosition => weaponPosition;
    
        private SomeStorageInt _currentDamageSprite;
        private Transform _playAreaLeftDownPivot;
    
        public void Initialization(PlayerSpaceshipLevelsConfig playerSpaceshipLevelsConfig)
        {
            if (!animator)
            {
                animator = GetComponentInChildren<Animator>();
                Debug.LogError("You dont Serialize animator");
            }

            _currentDamageSprite = new SomeStorageInt(damageAnimatorTriggerData.Count - 1);
        
            float healthPointsValue = playerSpaceshipLevelsConfig.LevelsHealthPoints[PlayerGlobalData.Instance.SpaceshipSettings.SpaceshipLevel - 1];
            healthPoints = new SomeStorageFloat(healthPointsValue,healthPointsValue);
        }
    
        protected override void OnStart()
        {
            base.OnStart();
        
            _playAreaLeftDownPivot = _playArea.LeftDownPivot;
        }
        
        public override void ChangeAnimatorState(bool animatorEnabled)
        {
            foreach (var stoppableAnimator in stoppableAnimators)
                stoppableAnimator.enabled = animatorEnabled;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            EnemySpaceshipBase enemySpaceshipBase = col.gameObject.GetComponent<EnemySpaceshipBase>();
            if (enemySpaceshipBase)
            {
                TakeDamage(enemySpaceshipBase.CollisionDamage);
                enemySpaceshipBase.TakeDamage(float.MaxValue);
            }
        }
    
        public void Move(Vector3 targetPosition)
        {
            if(!canMove) return;
            
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

            _eventBus.Invoke(new PlayerTakeDamage());
            if (_currentDamageSprite.CurrentValue >= 0 && _currentDamageSprite.CurrentValue < damageAnimatorTriggerData.Count)
            {
                if (!_currentDamageSprite.IsFull && damageAnimatorTriggerData[_currentDamageSprite.CurrentValue + 1].healthPointsPercent > healthPoints.FillingPercentage)
                {
                    _currentDamageSprite.ChangeCurrentValue(1);
                    animator.SetTrigger(damageAnimatorTriggerData[_currentDamageSprite.CurrentValue].animatorTriggerName.ToString());
                }
                else
                {
                    if (_currentDamageSprite.CurrentValue > 0 && damageAnimatorTriggerData[_currentDamageSprite.CurrentValue].healthPointsPercent <= healthPoints.FillingPercentage)
                    {
                        _currentDamageSprite.ChangeCurrentValue(-1);
                        animator.SetTrigger(damageAnimatorTriggerData[_currentDamageSprite.CurrentValue].animatorTriggerName.ToString());
                    }
                }
            }
        }
    
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
    }
}