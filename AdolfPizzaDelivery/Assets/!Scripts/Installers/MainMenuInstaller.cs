using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private ViewManager viewManager;
    public override void InstallBindings()
    {
        Container.Bind<ViewManager>().FromInstance(viewManager).AsSingle().NonLazy();
    }
}
