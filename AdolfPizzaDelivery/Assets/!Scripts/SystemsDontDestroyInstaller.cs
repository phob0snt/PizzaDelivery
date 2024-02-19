using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SystemsDontDestroyInstaller : MonoInstaller
{
    [SerializeField] private GameObject _SystemsPrefab;
    public override void InstallBindings()
    {
        DontDestroyOnLoad(Instantiate(_SystemsPrefab));
    }
}
