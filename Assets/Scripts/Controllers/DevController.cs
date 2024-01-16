using UnityEngine;

namespace Controllers
{
    public class DevController : ControllerBase
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
                PlayerPrefs.DeleteAll();
            
            if (Input.GetKeyDown(KeyCode.M))
                PlayerGlobalData.ChangeMoneyStarsCount(1000);
        }
    }
}