using UnityEngine;

namespace UI_System.UI_Elements
{
    public class RecorderCursor : MonoBehaviour
    {
        void Update()
        {
            transform.position = Input.mousePosition;
        }
    }
}
