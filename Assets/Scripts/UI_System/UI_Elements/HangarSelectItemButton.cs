using UnityEngine;
using Zenject;

namespace UI_System.UI_Elements
{
    public class HangarSelectItemButton : MonoBehaviour
    {
        [SerializeField] private HangarSelectItemEnum hangarSelectItemEnum;
        
        [Inject] private MainMenuHangarScreen _hangarScreen;

        public void _SelectWeaponForLevelUp()
        {
            if(_hangarScreen == null) Debug.LogWarning("null");
            _hangarScreen.SelectItem(hangarSelectItemEnum);
        }
    }
}
