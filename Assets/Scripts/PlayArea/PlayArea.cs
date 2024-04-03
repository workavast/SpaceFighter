using UnityEngine;

public class PlayArea : MonoBehaviour
{
    [SerializeField] private Transform leftDownPivot;

    public Transform LeftDownPivot => leftDownPivot;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IPlayAreaCollision playAreaCollision))
            playAreaCollision.EnterInPlayArea();
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IPlayAreaCollision playAreaCollision)) 
            playAreaCollision.ExitFromPlayerArea();
    }
}