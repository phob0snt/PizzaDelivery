using UnityEngine;
using Zenject;

public class ManagersInstaller : MonoInstaller
{
    [SerializeField] private DeliveryManager _Delivery;
    [SerializeField] private MoneyManager _Money;
    [SerializeField] private ViewManager _UI;
    public override void InstallBindings()
    {
        Container.Bind<DeliveryManager>().FromInstance(_Delivery).AsSingle().NonLazy();
        Container.Bind<MoneyManager>().FromInstance(_Money).AsSingle().NonLazy();
        Container.Bind<ViewManager>().FromInstance(_UI).AsSingle().NonLazy();
    }
}
