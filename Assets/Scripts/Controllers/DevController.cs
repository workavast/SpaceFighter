using Managers;
using Saves;
using UnityEngine;

namespace Controllers
{
    public class DevController : ControllerBase
    {
        [SerializeField] private AudioManager audioManager;
        private bool _pause = false;
     
#if UNITY_EDITOR   
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
                PlayerGlobalData.Instance.CoinsSettings.ChangeCoinsCount(1000);

            if (Input.GetKeyDown(KeyCode.P))
            {
                _pause = !_pause;
                audioManager.ChangeAudioState(_pause);
            }
        }
#endif
    }
}