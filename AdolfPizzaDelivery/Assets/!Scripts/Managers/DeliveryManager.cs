using Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class DeliveryManager : MonoBehaviour
{
    public static UnityEvent OnCreatedChain = new();
    public static UnityEvent OnCompletedChain = new();
    public static UnityEvent OnCompletedOrder = new();

    [HideInInspector] public OrderChain CurrentChain;
    [HideInInspector] public OrderState OrderState = OrderState.Default;
    public List<DeliveryPoint> DeliveryPoints;
    private List<DeliveryPoint> _pointsForIteration;
    [Inject] private ProgressManager _progressManager;
    

    public void CreateOrderChain()
    {
        CreateNewOrderChain(_progressManager.GetLevelMaxOrders());
        OnCreatedChain.Invoke();
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

    private void StartOrderChain()
    {
        Player.Instance.SpawnPizza(CurrentChain.orders.Count);
        CurrentChain.StartChain();
    }

    public DeliveryOrder FormOrder()
    {
        DeliveryOrder order = ScriptableObject.CreateInstance<DeliveryOrder>();
        order.Destination = _pointsForIteration[Random.Range(0, _pointsForIteration.Count)];
        _pointsForIteration.Remove(order.Destination);
        return order;
    }
}
