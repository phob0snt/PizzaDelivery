using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderChain : ScriptableObject
{
    public List<DeliveryOrder> orders = new();
    public int ordersInChain;
    public int ordersCompleted;

    private void OnEnable()
    {
        DeliveryManager.OnCompletedChain.AddListener(() => Destroy(this));
    }
    private void OnDisable()
    {
        DeliveryManager.OnCompletedChain.RemoveListener(() => Destroy(this));
    }
    public IEnumerator ProcessOrders()
    {
        ordersInChain = orders.Count;
        foreach (var order in orders)
        {
            order.ConfigureOrder();
            while (!order.isCompleted)
                yield return null;
            ordersCompleted++;
        }
    }
}
