using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Enums;
using Zenject;

public class DeliveryManager : MonoBehaviour
{
    public static UnityEvent OnCreatedChain = new();
    public static UnityEvent OnCompletedChain = new();
    public static UnityEvent<int, int> OnCompletedOrder = new();

    public PizzeriaLevel Level;
    [Inject] private MoneyManager moneyManager;

    [HideInInspector] public OrderChain currentChain;
    [HideInInspector] public OrderState orderState = OrderState.Default;
    public List<DeliveryPoint> DeliveryPoints;
    private List<DeliveryPoint> pointsForIteration;
    private void Awake()
    {
        Level = Resources.Load<PizzeriaLevel>("PizzeriaLevel");
    }

    public void CreateOrderChain()
    {
        CreateNewOrderChain(Random.Range(2, DeliveryPoints.Count));
        OnCreatedChain.Invoke();
        orderState = OrderState.Active;
    }

    public void CompleteOrderChain()
    {
        moneyManager.IncreaseMoney(moneyManager.GetOrderMoney()[1]);
        Level.AddProgress(moneyManager.GetOrderMoney()[0]);
        moneyManager.ClearOrderMoney();
        orderState = OrderState.Default;
        OnCompletedChain.Invoke();
    }
    
    private void CreateNewOrderChain(int pointsAmount)
    {
        if (pointsAmount > DeliveryPoints.Count)
            return;
        pointsForIteration = new();
        pointsForIteration.AddRange(DeliveryPoints);
        currentChain = ScriptableObject.CreateInstance<OrderChain>();
        for (int i = 0; i < pointsAmount; i++)
        {
            DeliveryOrder ord = FormOrder();
            ord.orderChain = currentChain;
            currentChain.orders.Add(ord);
        }
        Backpack.Instance.SpawnPizzaBoxes(pointsAmount);
        StartCoroutine(currentChain.ProcessOrders());
    }

    public DeliveryOrder FormOrder()
    {
        if (pointsForIteration.Count != 0)
        {
            DeliveryOrder order = ScriptableObject.CreateInstance<DeliveryOrder>();
            order.Destination = pointsForIteration[Random.Range(0, pointsForIteration.Count)];
            pointsForIteration.Remove(order.Destination);
            return order;
        }
        else
            return null;
    }
}
