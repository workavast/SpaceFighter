using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HangarSelectItemButton : MonoBehaviour
{
    [Inject] private MainMenuHangarScreen _hangarScreen;
    [SerializeField] private HangarSelectItemEnum hangarSelectItemEnum;

    public void _SelectWeaponForLevelUp()
    {
        _hangarScreen.SelectItem(hangarSelectItemEnum);
    }
}
