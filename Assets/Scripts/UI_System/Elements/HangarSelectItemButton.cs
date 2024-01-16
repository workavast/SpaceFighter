using UnityEngine;

namespace UI_System.Elements
{
    public class HangarSelectItemButton : MonoBehaviour
    {
        [SerializeField] private HangarSelectItemType hangarSelectItemType;
        [SerializeField] private MainMenuHangarScreen hangarScreen;

        public void _SelectWeaponForLevelUp()
        {
            if(hangarScreen == null) Debug.LogWarning("null");
            hangarScreen.SelectItem(hangarSelectItemType);
        }
    }
}
