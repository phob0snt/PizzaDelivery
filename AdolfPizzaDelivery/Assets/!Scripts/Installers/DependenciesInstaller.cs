using UnityEngine;
using Zenject;

public class DependenciesInstaller : MonoInstaller
{
    [SerializeField] private TouchCameraRotation _touchCam;
    [SerializeField] private DestinationArrow _arrow;
    [SerializeField] private Player _player;
    public override void InstallBindings()
    {
        Container.Bind<TouchCameraRotation>().FromInstance(_touchCam).AsSingle().NonLazy();
        Container.Bind<DestinationArrow>().FromInstance(_arrow).AsSingle().NonLazy();
        Container.Bind<Player>().FromInstance(_player).AsSingle().NonLazy();
    }
}
