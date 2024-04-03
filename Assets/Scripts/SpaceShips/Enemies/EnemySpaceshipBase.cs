using System;
using Configs.Missions;
using EventBusEvents;
using EventBusExtension;
using Managers;
using PathCreation;
using UnityEngine;
using Zenject;

namespace SpaceShips.Enemies
{
    public abstract class EnemySpaceshipBase : SpaceshipBase, PoolSystem.IPoolable<EnemySpaceshipBase, EnemySpaceshipType>
    {
        [Space]
        [SerializeField] private float collisionDamage = 1;
        [SerializeField] [Range(0, 2)] private float starsValueScale = 1;
        
        [Inject] private readonly PlayerSpaceshipManager _playerSpaceshipManager;
        [Inject] protected readonly EventBus EventBus;
    
        private EnemyAnimationController _enemyAnimationController;
        private EndOfPathInstruction _endOfPathInstruction;
        private EnemyRotationType _rotationType;
        private EnemyPathWayMoveType _moveType;
        private PathCreator _pathCreator;
        private Vector3 _prevPosition;
        private float _distanceTravelled;
        private float _moveSpeed;
        private bool _canMove;
        
        public abstract EnemySpaceshipType PoolId { get; }
        public float CollisionDamage => collisionDamage;

        protected event Action OnElementExtractFromPoolEvent;
        protected event Action OnHandleUpdate;
    
        public event Action<EnemySpaceshipBase> ReturnElementEvent;
        public event Action<EnemySpaceshipBase> DestroyElementEvent;
    
        protected override void OnAwake()
        {
            base.OnAwake();

            _moveSpeed = 0;

            OnDead += StartDying;
            _enemyAnimationController = GetComponentInChildren<EnemyAnimationController>();
            _enemyAnimationController.OnAwake(this);
        }

        public override void HandleUpdate(float time)
        {
            if(IsDead) return;
        
            if (_pathCreator && _canMove) Move(time);
            if (_pathCreator) Rotate();
        
            OnHandleUpdate?.Invoke();
        }

        public override void ChangeAnimatorState(bool animatorEnabled) 
            => _enemyAnimationController.ChangeAnimatorState(animatorEnabled);

        public void OnElementExtractFromPool()
        {
            IsDead = false;
        
            _distanceTravelled = 0;
            _moveSpeed = 0;
            _canMove = true;
        
            healthPoints.SetCurrentValue(healthPoints.MaxValue);
        
            gameObject.SetActive(true);
        
            OnElementExtractFromPoolEvent?.Invoke();
        }

        public void OnElementReturnInPool()
        {
            gameObject.SetActive(false);
        }
        
        public void EndDying()
            => ReturnElementEvent?.Invoke(this);

        public void SetWaveData(EnemyGroupConfig groupConfig)
            => SetWaveData(groupConfig.MoveSpeed, groupConfig.Path, groupConfig.PathWayMoveType, groupConfig.RotationType);
        
        public void SetWaveData(float newMoveSpeed, PathCreator newPath, EnemyPathWayMoveType newEnemyPathWayMoveType,
            EnemyRotationType newEnemyRotationType)
        {
            _moveSpeed = newMoveSpeed;
            ChangePathWay(newPath);
            _moveType = newEnemyPathWayMoveType;
            _rotationType = newEnemyRotationType;

            _endOfPathInstruction = newEnemyPathWayMoveType switch
            {
                EnemyPathWayMoveType.Loop => EndOfPathInstruction.Loop,
                EnemyPathWayMoveType.OnEndRemove => EndOfPathInstruction.Stop,
                EnemyPathWayMoveType.OnEndStop => EndOfPathInstruction.Stop,
                _ => throw new ArgumentOutOfRangeException(nameof(newEnemyPathWayMoveType), newEnemyPathWayMoveType, null)
            };

            transform.position = _pathCreator.path.GetPointAtDistance(0, _endOfPathInstruction);
        }
        
        private void Move(float time)
        {
            _distanceTravelled += _moveSpeed * time;
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

        private void OnPathUpdate() 
        {
            _distanceTravelled = _pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }

        private void StartDying()
        {
            if (IsDead) return;

            IsDead = true;
            EventBus.Invoke(new EnemyStartDie(transform.position, starsValueScale));
            _enemyAnimationController.SetDyingTrigger();
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
    }
}