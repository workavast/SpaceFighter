using GameCycle;
using UnityEngine;
using Zenject;

public class BackgroundMover : MonoBehaviour, IGameCycleUpdate
{
    [SerializeField] private GameObject background;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float swapDistance;
    
    [Inject] private readonly IGameCycleController _gameCycleController;

    private Vector2 _startPosition;
    
    private void Awake()
    {
        _startPosition = transform.position;
        _gameCycleController.AddListener(GameCycleState.Gameplay, this);
    }

    public void GameCycleUpdate()
    {
        var offset = direction.normalized * moveSpeed * Time.deltaTime;
        background.transform.Translate(offset);

        var curDistance = Vector2.Distance(_startPosition, transform.position);
        if (curDistance > swapDistance)
            background.transform.position = _startPosition + direction.normalized * (curDistance - swapDistance);
    }
}