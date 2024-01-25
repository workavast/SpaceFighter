using Managers;
using UnityEngine;

namespace Controllers
{
    public class DevController : ControllerBase
    {
        [SerializeField] private AudioManager audioManager;
        private bool _pause = false;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
                PlayerPrefs.DeleteAll();
            
            if (Input.GetKeyDown(KeyCode.M))
                PlayerGlobalData.ChangeCoinsCount(1000);

            if (Input.GetKeyDown(KeyCode.P))
            {
                _pause = !_pause;
                audioManager.ChangeAudioState(_pause);
            }
        }
    }
}