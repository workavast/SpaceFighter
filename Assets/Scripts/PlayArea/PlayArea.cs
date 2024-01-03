using UnityEngine;

public class PlayArea : MonoBehaviour
{
    [SerializeField] private Transform leftDownPivot;
    public Transform LeftDownPivot => leftDownPivot;

    public static PlayArea Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IPlayAreaCollision>(out IPlayAreaCollision projectile))
            projectile.EnterInPlayArea();
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<IPlayAreaCollision>(out IPlayAreaCollision projectile)) 
            projectile.ExitFromPlayerArea();
    }
}