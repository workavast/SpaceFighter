using UnityEngine;

namespace FrameRateCap
{
    public class FrameRateCap : MonoBehaviour
    {
        private static FrameRateCap _instance;
    
        private void Awake()
        {
            if (_instance)
            {
                Destroy(this);
                return;
            }

            _instance = this;
            Application.targetFrameRate = 60;
            DontDestroyOnLoad(this);
        }
    }
}
