using Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class DeliveryManager : MonoBehaviour
{
    public static UnityEvent OnCreatedChain = new();
    public static UnityEvent OnCompletedChain = new();
    public static UnityEvent<DeliveryOrder> OnCompletedOrder = new();

    [HideInInspector] public OrderChain CurrentChain;
    [HideInInspector] public OrderState OrderState = OrderState.Default;
    public List<DeliveryPoint> DeliveryPoints;
    private List<DeliveryPoint> _pointsForIteration;

    [Inject] private ProgressManager _progressManager;
    [Inject] private ViewManager _viewManager;

    public void CreateOrderChain()
    {
        if (Player.Instance.Backpack.Capability < _progressManager.GetLevelMaxOrders())
            CreateNewOrderChain(Player.Instance.Backpack.Capability);
        else
            CreateNewOrderChain(_progressManager.GetLevelMaxOrders());
        OrderState = OrderState.Active;
    }

    public void CompleteOrderChain()
    {
        OrderState = OrderState.Default;
        _progressManager.AddLevelProgress();
        OnCompletedChain.Invoke();
    }
    
    private void CreateNewOrderChain(int pointsAmount)
    {
        _pointsForIteration = new();
        _pointsForIteration.AddRange(DeliveryPoints);
        CurrentChain = ScriptableObject.CreateInstance<OrderChain>();
        for (int i = 0; i < pointsAmount; i++)
        {
            DeliveryOrder ord = FormOrder();
            ord.OrderChain = CurrentChain;
            CurrentChain.orders.Add(ord);
        }
        StartOrderChain();
    }
    private DeliveryOrder FormOrder()
    {
        DeliveryOrder order = ScriptableObject.CreateInstance<DeliveryOrder>();
        int destinationNum = Random.Range(0, _pointsForIteration.Count);
        order.Destination = _pointsForIteration[destinationNum];
        order.Destination.SetPointNumber(DeliveryPoints.IndexOf(_pointsForIteration[destinationNum]));
        _viewManager.GetView<MapView>().AddPointToMark(DeliveryPoints.IndexOf(_pointsForIteration[destinationNum]));
        _pointsForIteration.Remove(order.Destination);
        return order;
    }

    private void StartOrderChain()
    {
        Player.Instance.SpawnPizza(CurrentChain.orders.Count);
        CurrentChain.StartChain();
        OnCreatedChain.Invoke();
    }
}
