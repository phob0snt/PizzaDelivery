using UnityEngine;
using Zenject;

public class SystemsInstaller : MonoInstaller
{
    [SerializeField] private GameObject _Systems;
    public override void InstallBindings()
    {
        DontDestroyOnLoad(Instantiate(_Systems));
    }
}