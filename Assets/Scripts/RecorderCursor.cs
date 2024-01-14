using UnityEngine;

public class RecorderCursor : MonoBehaviour
{
    void Update()
    {
      transform.position = Input.mousePosition;
    }
}
