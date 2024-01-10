using UnityEngine;

public class PlayArea : MonoBehaviour
{
    [SerializeField] private Transform leftDownPivot;
    public Transform LeftDownPivot => leftDownPivot;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IPlayAreaCollision projectile))
            projectile.EnterInPlayArea();
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out IPlayAreaCollision projectile)) 
            projectile.ExitFromPlayerArea();
    }
}