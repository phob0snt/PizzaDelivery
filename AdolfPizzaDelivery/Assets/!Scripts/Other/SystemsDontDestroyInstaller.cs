using UnityEngine;
using Zenject;

public class SystemsDontDestroyInstaller : MonoInstaller
{
    [SerializeField] private GameObject _systemsPrefab;
    public override void InstallBindings()
    {
        DontDestroyOnLoad(Instantiate(_systemsPrefab));
    }
}
