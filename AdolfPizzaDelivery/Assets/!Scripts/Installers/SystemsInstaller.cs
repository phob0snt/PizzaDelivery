using UnityEngine;
using Zenject;

public class SystemsInstaller : MonoInstaller
{
    [SerializeField] private GameObject _systems;
    public override void InstallBindings()
    {
        DontDestroyOnLoad(Instantiate(_systems));
    }
}