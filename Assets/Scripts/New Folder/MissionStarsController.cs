using System;
using System.Collections;
using System.Collections.Generic;
using SomeStorages;
using UnityEngine;
using Zenject.SpaceFighter;

public class MissionStarsController : MonoBehaviour
{
    private MissionCycleController _missionCycleController;
    private PlayerSpaceship _playerSpaceship;
    private EnemySpaceshipsManager _enemyManager;
    
    private int _currentStarsNum = 3;

    private void Awake()
    {
        _missionCycleController.OnWinState += ApplyStars;
        _playerSpaceship.OnTakeDamage += LostStar;
    }

    private void LostStar()
    {
        _playerSpaceship.OnTakeDamage -= LostStar;
        _currentStarsNum--;
        if (_currentStarsNum < 0) _currentStarsNum = 0;
        Debug.Log("You lose one star");
    }

    private void ApplyStars()
    {
        PlayerGlobalData.ChangeMissionData(0, _currentStarsNum);
    }
}
