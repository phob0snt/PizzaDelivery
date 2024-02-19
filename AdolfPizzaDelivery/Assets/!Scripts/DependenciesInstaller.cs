using UnityEngine;
using Zenject;

public class DependenciesInstaller : MonoInstaller
{
    [SerializeField] private TouchCameraRotation touchCam;
    [SerializeField] private DestinationArrow arrow;
    public override void InstallBindings()
    {
        Container.Bind<TouchCameraRotation>().FromInstance(touchCam).AsSingle().NonLazy();
        Container.Bind<DestinationArrow>().FromInstance(arrow).AsSingle().NonLazy();
    }
}
