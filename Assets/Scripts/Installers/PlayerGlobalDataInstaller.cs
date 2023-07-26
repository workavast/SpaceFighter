using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerGlobalDataInstaller : MonoInstaller
{
    private PlayerGlobalData _playerGlobalData = new PlayerGlobalData();
    
    public override void InstallBindings()
    {
        _playerGlobalData.LoadData();
        Container.Bind<PlayerGlobalData>().FromInstance(_playerGlobalData).AsSingle();
    }
}
