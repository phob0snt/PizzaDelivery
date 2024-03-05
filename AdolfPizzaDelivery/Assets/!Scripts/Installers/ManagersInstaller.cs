using UnityEngine;
using Zenject;

public class ManagersInstaller : MonoInstaller
{
    [SerializeField] private DeliveryManager _Delivery;
    [SerializeField] private MoneyManager _Money;
    [SerializeField] private ViewManager _UI;
    [SerializeField] private TerrainLayerManager _Terrain;
    [SerializeField] private ProgressManager _Progress;

    public override void InstallBindings()
    {
        Container.Bind<DeliveryManager>().FromInstance(_Delivery).AsSingle().NonLazy();
        Container.Bind<MoneyManager>().FromInstance(_Money).AsSingle().NonLazy();
        Container.Bind<ViewManager>().FromInstance(_UI).AsSingle().NonLazy();
        Container.Bind<TerrainLayerManager>().FromInstance(_Terrain).AsSingle().NonLazy();
        Container.Bind<ProgressManager>().FromInstance(_Progress).AsSingle().NonLazy();
    }
}
