using Configs;
using PlayerWeapon;
using Saves;
using TMPro;
using UI_System.Elements;
using UnityEngine;
using Zenject;

namespace UI_System.UI_Screens.MainMenu
{
    public class MainMenuHangarScreen : UI_ScreenBase
    {
        [SerializeField] private TextMeshProUGUI coinsCount;
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
        [SerializeField] private EquipButton equipButton;
        [SerializeField] private LevelUpButton levelUpButton;

        [Inject] private PlayerWeaponConfig _playerWeaponConfig;
        [Inject] private PlayerSpaceshipLevelsConfig _playerSpaceshipLevelsConfig;
    
        private HangarSelectItemType _currentHangarSelectItem;

        private void Awake()
        {
            equipButton.Init();
            levelUpButton.Init();
        }

        private void Start()
        {
            _currentHangarSelectItem = HangarSelectItemType.Spaceship;
            UpdateScreen();
        }

    
        #region UpdateScreen

        private void UpdateScreen()
        {
            Debug.Log("-||- UpdateMoneyStarsCount");
            UpdateMoneyStarsCount();
            Debug.Log("-||- UpdateWeaponsLocks");
            UpdateWeaponsLocks();
            Debug.Log("-||- UpdateEquippedWeapon");
            UpdateEquippedWeapon();
            Debug.Log("-||- UpdateSelectedItem");
            UpdateSelectedItem();
            Debug.Log("-||- UpdateSelectOrSelectedText");
            UpdateSelectOrSelectedText();
            Debug.Log("-||- UpdateBuyOrLevelUpOrMaxLevelText");
            UpdateBuyOrLevelUpOrMaxLevelText();
        }

        private void UpdateMoneyStarsCount()
        {
            coinsCount.text = $"{PlayerGlobalData.Instance.CoinsSettings.CoinsCount}";
        }

        private void UpdateWeaponsLocks()
        {
            //TODO: fix bug: throw exception "KeyNotFoundException: The given key 'AutoCannon' was not present in the dictionary."
            autoCannonLock.SetActive(PlayerGlobalData.Instance.WeaponsSettings.CurrentWeaponsLevels[PlayerWeaponType.AutoCannon] <= 0);
            bigSpaceGunLock.SetActive(PlayerGlobalData.Instance.WeaponsSettings.CurrentWeaponsLevels[PlayerWeaponType.BigSpaceGun] <= 0);
            rocketsLock.SetActive(PlayerGlobalData.Instance.WeaponsSettings.CurrentWeaponsLevels[PlayerWeaponType.Rockets] <= 0);
            zapperLock.SetActive(PlayerGlobalData.Instance.WeaponsSettings.CurrentWeaponsLevels[PlayerWeaponType.Zapper] <= 0);
        }
    
        private void UpdateEquippedWeapon()
        {
            autoCannonVisualisation.SetActive(false);
            bigSpaceGunVisualisation.SetActive(false);
            rocketsVisualisation.SetActive(false);
            zapperVisualisation.SetActive(false);
        
            switch (PlayerGlobalData.Instance.WeaponsSettings.EquippedPlayerWeapon)
            {
                case PlayerWeaponType.AutoCannon:
                    autoCannonVisualisation.SetActive(true);
                    break;
                case PlayerWeaponType.BigSpaceGun:
                    bigSpaceGunVisualisation.SetActive(true);
                    break;
                case PlayerWeaponType.Rockets:
                    rocketsVisualisation.SetActive(true);
                    break;
                case PlayerWeaponType.Zapper:
                    zapperVisualisation.SetActive(true);
                    break;
                default:
                    autoCannonVisualisation.SetActive(true);
                    Debug.LogError("Undefined PlayerWeaponType");
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
                case HangarSelectItemType.Spaceship: spaceshipSelect.SetActive(true); break;
                case HangarSelectItemType.AutoCannon: autoCannonSelect.SetActive(true); break;
                case HangarSelectItemType.BigSpaceGun: bigSpaceGunSelect.SetActive(true); break;
                case HangarSelectItemType.Rockets: rocketsSelect.SetActive(true); break;
                case HangarSelectItemType.Zapper: zapperSelect.SetActive(true); break;
                default: Debug.LogError("Undefined UpdateSelectedItem"); break;        
            }
        }

        private void UpdateSelectOrSelectedText()
        {
            PlayerWeaponType weaponType;
            switch (_currentHangarSelectItem)
            {
                case HangarSelectItemType.Spaceship: equipButton.SetEquippedText(); return;
                case HangarSelectItemType.AutoCannon: weaponType = PlayerWeaponType.AutoCannon; break;
                case HangarSelectItemType.BigSpaceGun: weaponType = PlayerWeaponType.BigSpaceGun; break;
                case HangarSelectItemType.Rockets: weaponType = PlayerWeaponType.Rockets; break;
                case HangarSelectItemType.Zapper: weaponType = PlayerWeaponType.Zapper; break;
                default: Debug.LogError("Undefined HangarSelectItemEnum"); return;
            }
        
            if (PlayerGlobalData.Instance.WeaponsSettings.CurrentWeaponsLevels[weaponType] <= 0)
                equipButton.SetLockedText();
            else
            {
                if (weaponType == PlayerGlobalData.Instance.WeaponsSettings.EquippedPlayerWeapon) 
                    equipButton.SetEquippedText();
                else 
                    equipButton.SetEquipText();
            }
        }
    
        private void UpdateBuyOrLevelUpOrMaxLevelText()
        {
            PlayerWeaponType weaponType;
            switch (_currentHangarSelectItem)
            {
                case HangarSelectItemType.Spaceship:
                    int currentSpaceshipLevel = PlayerGlobalData.Instance.SpaceshipSettings.SpaceshipLevel;
                    if (currentSpaceshipLevel >= _playerSpaceshipLevelsConfig.LevelsCount)
                    {
                        levelUpButton.SetMaxLevelText();
                        return;
                    }
                
                    if (currentSpaceshipLevel <= 0)
                        levelUpButton.SetBuyText(_playerSpaceshipLevelsConfig.LevelsPrices[currentSpaceshipLevel]);
                    else
                        levelUpButton.SetLevelUpText(_playerSpaceshipLevelsConfig.LevelsPrices[currentSpaceshipLevel]);
                    return;

                case HangarSelectItemType.AutoCannon: weaponType = PlayerWeaponType.AutoCannon; break;
                case HangarSelectItemType.BigSpaceGun: weaponType = PlayerWeaponType.BigSpaceGun; break;
                case HangarSelectItemType.Rockets: weaponType = PlayerWeaponType.Rockets; break;
                case HangarSelectItemType.Zapper: weaponType = PlayerWeaponType.Zapper; break;
                default: Debug.LogError("Undefined HangarSelectItemType"); return;
            }
        
            if (!PlayerGlobalData.Instance.WeaponsSettings.CurrentWeaponsLevels.ContainsKey(weaponType))
            {
                Debug.LogError("Undefined PlayerWeaponType");
                return;
            }

            int currentSelectedWeaponLevel = PlayerGlobalData.Instance.WeaponsSettings.CurrentWeaponsLevels[weaponType];
            if ( currentSelectedWeaponLevel >= _playerWeaponConfig.WeaponsLevelsData[weaponType].Count)
            {
                levelUpButton.SetMaxLevelText();
                return;
            }
        
            if (currentSelectedWeaponLevel <= 0)
                levelUpButton.SetBuyText(_playerWeaponConfig.WeaponPricesData[weaponType][currentSelectedWeaponLevel]);
            else
                levelUpButton.SetLevelUpText(_playerWeaponConfig.WeaponPricesData[weaponType][currentSelectedWeaponLevel]);
        }
    
        #endregion
    
    
        #region Buttons
    
        public void SelectItem(HangarSelectItemType hangarSelectItem)
        {
            _currentHangarSelectItem = hangarSelectItem;
            UpdateScreen();
        }
    
        public void _TryChangeEquippedWeapon()
        {
            PlayerWeaponType playerWeapon;
            switch (_currentHangarSelectItem)
            {
                case HangarSelectItemType.Spaceship:
                    return;
                case HangarSelectItemType.AutoCannon:
                    playerWeapon = PlayerWeaponType.AutoCannon;
                    break;
                case HangarSelectItemType.BigSpaceGun:
                    playerWeapon = PlayerWeaponType.BigSpaceGun;
                    break;
                case HangarSelectItemType.Rockets:
                    playerWeapon = PlayerWeaponType.Rockets;
                    break;
                case HangarSelectItemType.Zapper:
                    playerWeapon = PlayerWeaponType.Zapper;
                    break;
                default:
                    Debug.LogError("Undefined HangarSelectItemEnum(PlayerWeaponsEnum)");
                    playerWeapon = PlayerWeaponType.AutoCannon;
                    break;
            }
        
            if (!PlayerGlobalData.Instance.WeaponsSettings.CurrentWeaponsLevels.ContainsKey(playerWeapon))
            {
                Debug.LogError("Undefined weapon enum");
                return;
            }
        
            if (PlayerGlobalData.Instance.WeaponsSettings.EquippedPlayerWeapon == playerWeapon)
                return;

            if(PlayerGlobalData.Instance.WeaponsSettings.CurrentWeaponsLevels[playerWeapon] <= 0)
                return;
        
            PlayerGlobalData.Instance.WeaponsSettings.ChangeEquippedWeapon(playerWeapon);
            UpdateScreen();
        }

        public void _TryLevelUpItem()
        {
            switch (_currentHangarSelectItem)
            {
                case HangarSelectItemType.Spaceship:
                    TryLevelUpSpaceship();
                    break;
                case HangarSelectItemType.AutoCannon:
                    TryLevelUpWeapon(PlayerWeaponType.AutoCannon);
                    break;
                case HangarSelectItemType.BigSpaceGun:
                    TryLevelUpWeapon(PlayerWeaponType.BigSpaceGun);
                    break;
                case HangarSelectItemType.Rockets:
                    TryLevelUpWeapon(PlayerWeaponType.Rockets);
                    break;
                case HangarSelectItemType.Zapper:
                    TryLevelUpWeapon(PlayerWeaponType.Zapper);
                    break;
                default:
                    Debug.LogError("Undefined HangarSelectItemEnum");
                    return;
            }
        
            UpdateScreen();
        }

        private void TryLevelUpSpaceship()
        {
            int currentSpaceshipLevel = PlayerGlobalData.Instance.SpaceshipSettings.SpaceshipLevel;
            if(currentSpaceshipLevel >= _playerSpaceshipLevelsConfig.LevelsCount){
                Debug.LogWarning("Maximal spaceship level");
                return;
            }

            int levelUpPrice = _playerSpaceshipLevelsConfig.LevelsPrices[currentSpaceshipLevel];
            if (levelUpPrice > PlayerGlobalData.Instance.CoinsSettings.CoinsCount)
            {
                Debug.LogWarning("Not enough coins");
                return;
            }
        
            PlayerGlobalData.Instance.SpaceshipSettings.IncreaseSpaceshipLevel();
            PlayerGlobalData.Instance.CoinsSettings.ChangeCoinsCount(-levelUpPrice);
            UpdateScreen();
        }
    
        private void TryLevelUpWeapon(PlayerWeaponType playerWeapon)
        {
            if (!PlayerGlobalData.Instance.WeaponsSettings.CurrentWeaponsLevels.ContainsKey(playerWeapon))
            {
                Debug.LogError("Undefined weapon enum");
                return;
            }
        
            int currentSelectedWeaponLevel = PlayerGlobalData.Instance.WeaponsSettings.CurrentWeaponsLevels[playerWeapon];
            if (currentSelectedWeaponLevel >= _playerWeaponConfig.WeaponsLevelsData[playerWeapon].Count)
                return;
            
            int levelUpPrice = _playerWeaponConfig.WeaponPricesData[playerWeapon][currentSelectedWeaponLevel];
            if (levelUpPrice <= PlayerGlobalData.Instance.CoinsSettings.CoinsCount)
            {
                PlayerGlobalData.Instance.WeaponsSettings.LevelUpWeapon(playerWeapon);
                PlayerGlobalData.Instance.CoinsSettings.ChangeCoinsCount(-levelUpPrice);
            }
            else
            {
                Debug.LogWarning("Not enough stars money");
            }
        }
        #endregion
    }
}
