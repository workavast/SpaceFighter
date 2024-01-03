using UnityEngine;
using Zenject;

public class HangarSelectItemButton : MonoBehaviour
{
    [Inject] private MainMenuHangarScreen _hangarScreen;
    [SerializeField] private HangarSelectItemEnum hangarSelectItemEnum;

    public void _SelectWeaponForLevelUp()
    {
        if(_hangarScreen == null) Debug.LogWarning("null");
        _hangarScreen.SelectItem(hangarSelectItemEnum);
    }
}
