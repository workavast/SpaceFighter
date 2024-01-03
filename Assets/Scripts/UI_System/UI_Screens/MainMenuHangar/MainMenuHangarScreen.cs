using TMPro;
using UnityEngine;
using Zenject;

public enum HangarSelectItemEnum
{
    Spaceship,
    AutoCannon,
    BigSpaceGun,
    Rockets,
    Zapper
}

public class MainMenuHangarScreen : UI_ScreenBase
{
    [SerializeField] private TextMeshProUGUI moneyStarsCount;
    [Space]
    [SerializeField] private GameObject autoCannonVisualisation;
    [SerializeField] private GameObject bigSpaceGunVisualisation;
    [SerializeField] private GameObject rocketsVisualisation;
    [SerializeField] private GameObject zapperVisualisation;
    [Space]
    [SerializeField] private GameObject autoCannonLock;
    [SerializeField] private GameObject bigSpaceGunLock;
    [SerializeField] private GameObject rocketsLock;
    [SerializeField] private GameObject zapperLock;
    [Space]
    [SerializeField] private GameObject spaceshipSelect;
    [SerializeField] private GameObject autoCannonSelect;
    [SerializeField] private GameObject bigSpaceGunSelect;
    [SerializeField] private GameObject rocketsSelect;
    [SerializeField] private GameObject zapperSelect;
    [Space] 
    [SerializeField] private TextMeshProUGUI equipOrEquippedText;
    [SerializeField] private TextMeshProUGUI buyOrLevelUpOrMaxLevelText;

    [Inject] private PlayerWeaponConfig _playerWeaponConfig;
    [Inject] private PlayerSpaceshipLevelsConfig _playerSpaceshipLevelsConfig;
    
    private HangarSelectItemEnum _currentHangarSelectItem;
    
    private void Start()
    {
        _currentHangarSelectItem = HangarSelectItemEnum.Spaceship;
        UpdateScreen();
    }

    
    #region UpdateScreen

    private void UpdateScreen()
    {
        UpdateMoneyStarsCount();
        UpdateWeaponsLocks();
        UpdateEquippedWeapon();
        UpdateSelectedItem();
        UpdateSelectOrSelectedText();
        UpdateBuyOrLevelUpOrMaxLevelText();
    }

    private void UpdateMoneyStarsCount()
    {
        moneyStarsCount.text = "" + PlayerGlobalData.MoneyStarsCount;
    }

    private void UpdateWeaponsLocks()
    {
        autoCannonLock.SetActive(PlayerGlobalData.CurrentWeaponsLevels[PlayerWeaponsEnum.AutoCannon] <= 0);
        bigSpaceGunLock.SetActive(PlayerGlobalData.CurrentWeaponsLevels[PlayerWeaponsEnum.BigSpaceGun] <= 0);
        rocketsLock.SetActive(PlayerGlobalData.CurrentWeaponsLevels[PlayerWeaponsEnum.Rockets] <= 0);
        zapperLock.SetActive(PlayerGlobalData.CurrentWeaponsLevels[PlayerWeaponsEnum.Zapper] <= 0);
    }
    
    private void UpdateEquippedWeapon()
    {
        autoCannonVisualisation.SetActive(false);
        bigSpaceGunVisualisation.SetActive(false);
        rocketsVisualisation.SetActive(false);
        zapperVisualisation.SetActive(false);
        
        switch (PlayerGlobalData.EquippedPlayerWeapon)
        {
            case PlayerWeaponsEnum.AutoCannon:
                autoCannonVisualisation.SetActive(true);
                break;
            case PlayerWeaponsEnum.BigSpaceGun:
                bigSpaceGunVisualisation.SetActive(true);
                break;
            case PlayerWeaponsEnum.Rockets:
                rocketsVisualisation.SetActive(true);
                break;
            case PlayerWeaponsEnum.Zapper:
                zapperVisualisation.SetActive(true);
                break;
            default:
                autoCannonVisualisation.SetActive(true);
                Debug.LogWarning("Undefined PlayerWeaponsEnum");
                break;
        }
    }

    private void UpdateSelectedItem()
    {
        spaceshipSelect.SetActive(false);
        autoCannonSelect.SetActive(false);
        bigSpaceGunSelect.SetActive(false);
        rocketsSelect.SetActive(false);
        zapperSelect.SetActive(false);

        switch (_currentHangarSelectItem)
        {
            case HangarSelectItemEnum.Spaceship: spaceshipSelect.SetActive(true); break;
            case HangarSelectItemEnum.AutoCannon: autoCannonSelect.SetActive(true); break;
            case HangarSelectItemEnum.BigSpaceGun: bigSpaceGunSelect.SetActive(true); break;
            case HangarSelectItemEnum.Rockets: rocketsSelect.SetActive(true); break;
            case HangarSelectItemEnum.Zapper: zapperSelect.SetActive(true); break;
            default:
                Debug.LogWarning("Undefined UpdateSelectedItem");
                break;        
        }
    }

    private void UpdateSelectOrSelectedText()
    {
        // if (PlayerGlobalData.CurrentWeaponsLevels[_currentSelectedWeapon] <= 0)
        //     equipOrEquippedText.text = "Locked";
        // else
        //     equipOrEquippedText.text = _currentSelectedWeapon == PlayerGlobalData.EquippedPlayerWeapon
        //         ? "Equipped"
        //         : "Equip";
        
        PlayerWeaponsEnum weaponsEnum;
        switch (_currentHangarSelectItem)
        {
            case HangarSelectItemEnum.Spaceship:
                equipOrEquippedText.text = "Equipped";
                return;

            case HangarSelectItemEnum.AutoCannon: weaponsEnum = PlayerWeaponsEnum.AutoCannon; break;
            case HangarSelectItemEnum.BigSpaceGun: weaponsEnum = PlayerWeaponsEnum.BigSpaceGun; break;
            case HangarSelectItemEnum.Rockets: weaponsEnum = PlayerWeaponsEnum.Rockets; break;
            case HangarSelectItemEnum.Zapper: weaponsEnum = PlayerWeaponsEnum.Zapper; break;
            default:
                Debug.LogWarning("Undefined HangarSelectItemEnum");
                return;
        }
        
        if (PlayerGlobalData.CurrentWeaponsLevels[weaponsEnum] <= 0)
            equipOrEquippedText.text = "Locked";
        else
            equipOrEquippedText.text = weaponsEnum == PlayerGlobalData.EquippedPlayerWeapon
                ? "Equipped"
                : "Equip";
    }
    
    private void UpdateBuyOrLevelUpOrMaxLevelText()
    {
        // if (!PlayerGlobalData.CurrentWeaponsLevels.ContainsKey(_currentSelectedWeapon))
        // {
        //     Debug.LogWarning("Undefined weapon enum");
        //     return;
        // }
        //
        // int currentSelectedWeaponLevel = PlayerGlobalData.CurrentWeaponsLevels[_currentSelectedWeapon];
        // if ( currentSelectedWeaponLevel >= _playerWeaponConfig.WeaponsLevelsData[_currentSelectedWeapon].Count)
        // {
        //     buyOrLevelUpOrMaxLevelText.text = "Max level";
        //     return;
        // }
        //
        // buyOrLevelUpOrMaxLevelText.text = currentSelectedWeaponLevel <= 0 ? "Buy" : "Level Up";
        // buyOrLevelUpOrMaxLevelText.text += "\nby " + _playerWeaponConfig.WeaponPricesData[_currentSelectedWeapon][currentSelectedWeaponLevel];
        
        
        PlayerWeaponsEnum weaponsEnum;
        switch (_currentHangarSelectItem)
        {
            case HangarSelectItemEnum.Spaceship:
                int currentSpaceshipLevel = PlayerGlobalData.CurrentSpaceshipLevel;
                if (currentSpaceshipLevel >= _playerSpaceshipLevelsConfig.LevelsCount)
                {
                    buyOrLevelUpOrMaxLevelText.text = "Max level";
                    return;
                }
        
                buyOrLevelUpOrMaxLevelText.text = currentSpaceshipLevel <= 0 ? "Buy" : "Level Up";
                buyOrLevelUpOrMaxLevelText.text += "\nby " + _playerSpaceshipLevelsConfig.LevelsPrices[currentSpaceshipLevel];
                return;

            case HangarSelectItemEnum.AutoCannon: weaponsEnum = PlayerWeaponsEnum.AutoCannon; break;
            case HangarSelectItemEnum.BigSpaceGun: weaponsEnum = PlayerWeaponsEnum.BigSpaceGun; break;
            case HangarSelectItemEnum.Rockets: weaponsEnum = PlayerWeaponsEnum.Rockets; break;
            case HangarSelectItemEnum.Zapper: weaponsEnum = PlayerWeaponsEnum.Zapper; break;
            default:
                Debug.LogWarning("Undefined HangarSelectItemEnum");
                return;
        }
        
        if (!PlayerGlobalData.CurrentWeaponsLevels.ContainsKey(weaponsEnum))
        {
            Debug.LogWarning("Undefined weapon enum");
            return;
        }

        int currentSelectedWeaponLevel = PlayerGlobalData.CurrentWeaponsLevels[weaponsEnum];
        if ( currentSelectedWeaponLevel >= _playerWeaponConfig.WeaponsLevelsData[weaponsEnum].Count)
        {
            buyOrLevelUpOrMaxLevelText.text = "Max level";
            return;
        }
        
        buyOrLevelUpOrMaxLevelText.text = currentSelectedWeaponLevel <= 0 ? "Buy" : "Level Up";
        buyOrLevelUpOrMaxLevelText.text += "\nby " + _playerWeaponConfig.WeaponPricesData[weaponsEnum][currentSelectedWeaponLevel];
    }
    
    #endregion
    
    
    #region Buttons
    
    public void SelectItem(HangarSelectItemEnum hangarSelectItem)
    {
        Debug.Log("selected " + hangarSelectItem);
        _currentHangarSelectItem = hangarSelectItem;
        UpdateScreen();
    }
    
    public void _TryChangeEquippedWeapon()
    {
        PlayerWeaponsEnum playerWeapon;
        switch (_currentHangarSelectItem)
        {
            case HangarSelectItemEnum.Spaceship:
                Debug.LogWarning("You try equip spaceship");
                return;
            case HangarSelectItemEnum.AutoCannon:
                playerWeapon = PlayerWeaponsEnum.AutoCannon;
                break;
            case HangarSelectItemEnum.BigSpaceGun:
                playerWeapon = PlayerWeaponsEnum.BigSpaceGun;
                break;
            case HangarSelectItemEnum.Rockets:
                playerWeapon = PlayerWeaponsEnum.Rockets;
                break;
            case HangarSelectItemEnum.Zapper:
                playerWeapon = PlayerWeaponsEnum.Zapper;
                break;
            default:
                Debug.LogWarning("Undefined HangarSelectItemEnum(PlayerWeaponsEnum)");
                playerWeapon = PlayerWeaponsEnum.AutoCannon;
                break;
        }
        
        if (!PlayerGlobalData.CurrentWeaponsLevels.ContainsKey(playerWeapon))
        {
            Debug.LogWarning("Undefined weapon enum");
            return;
        }
        if (PlayerGlobalData.EquippedPlayerWeapon == playerWeapon)
        {
            Debug.LogWarning("Weapon already equipped");
            return;
        }
        if(PlayerGlobalData.CurrentWeaponsLevels[playerWeapon] <= 0)
        {
            Debug.LogWarning("Weapon locked");
            return;
        }        
        
        PlayerGlobalData.ChangeEquippedWeapon(playerWeapon);
        UpdateScreen();
    }

    public void _TryLevelUpItem()
    {
        switch (_currentHangarSelectItem)
        {
            case HangarSelectItemEnum.Spaceship:
                TryLevelUpSpaceship();
                break;
            case HangarSelectItemEnum.AutoCannon:
                TryLevelUpWeapon(PlayerWeaponsEnum.AutoCannon);
                break;
            case HangarSelectItemEnum.BigSpaceGun:
                TryLevelUpWeapon(PlayerWeaponsEnum.BigSpaceGun);
                break;
            case HangarSelectItemEnum.Rockets:
                TryLevelUpWeapon(PlayerWeaponsEnum.Rockets);
                break;
            case HangarSelectItemEnum.Zapper:
                TryLevelUpWeapon(PlayerWeaponsEnum.Zapper);
                break;
            default:
                Debug.LogWarning("Undefined HangarSelectItemEnum");
                return;
        }
        
        UpdateScreen();
    }

    private void TryLevelUpSpaceship()
    {
        int currentSpaceshipLevel = PlayerGlobalData.CurrentSpaceshipLevel;
        if(currentSpaceshipLevel >= _playerSpaceshipLevelsConfig.LevelsCount){
            Debug.LogWarning("Maximal spaceship level");
            return;
        }

        int levelUpPrice = _playerSpaceshipLevelsConfig.LevelsPrices[currentSpaceshipLevel];
        if (levelUpPrice > PlayerGlobalData.MoneyStarsCount)
        {
            Debug.LogWarning("Not enough money stars count");
            return;
        }
        
        PlayerGlobalData.LevelUpSpaceship();
        PlayerGlobalData.ChangeMoneyStarsCount(-levelUpPrice);
        UpdateScreen();
    }
    
    private void TryLevelUpWeapon(PlayerWeaponsEnum playerWeapon)
    {
        if (!PlayerGlobalData.CurrentWeaponsLevels.ContainsKey(playerWeapon))
        {
            Debug.LogWarning("Undefined weapon enum");
            return;
        }
        
        int currentSelectedWeaponLevel = PlayerGlobalData.CurrentWeaponsLevels[playerWeapon];
        if (currentSelectedWeaponLevel >= _playerWeaponConfig.WeaponsLevelsData[playerWeapon].Count)
        {
            Debug.LogWarning("Maximal level"); 
            return;
        }
            
        int levelUpPrice = _playerWeaponConfig.WeaponPricesData[playerWeapon][currentSelectedWeaponLevel];
        if (levelUpPrice <= PlayerGlobalData.MoneyStarsCount)
        {
            PlayerGlobalData.LevelUpWeapon(playerWeapon);
            PlayerGlobalData.ChangeMoneyStarsCount(-levelUpPrice);
        }
        else
        {
            Debug.LogWarning("Not enough stars money");
        }
    }
    #endregion
}
