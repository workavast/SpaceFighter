using UnityEngine;

namespace UI_System.Elements
{
    public class RecorderCursor : MonoBehaviour
    {
        void Update()
        {
            transform.position = Input.mousePosition;
        }
    }
}
