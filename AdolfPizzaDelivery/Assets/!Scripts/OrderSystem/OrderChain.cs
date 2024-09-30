using System.Collections.Generic;
using UnityEngine;

public class OrderChain : ScriptableObject
{
    public List<DeliveryOrder> orders = new();
    public int OrdersCompleted { get; private set; }
    private bool _isActive = false;

    private void OnEnable()
    {
        DeliveryManager.OnCompletedChain.AddListener(() => Destroy(this));
    }
    private void OnDisable()
    {
        DeliveryManager.OnCompletedChain.RemoveListener(() => Destroy(this));
    }

    public void StartChain()
    {
        if (_isActive)
            Debug.LogAssertion("OrderChain is already in active state");
        else
        {
            _isActive = true;
            foreach (var order in orders)
            {
                order.Configure();
            }
        }
    }

    public void CompleteOrder(DeliveryOrder order)
    {
        DeliveryManager.OnCompletedOrder.Invoke(order);
        Destroy(order);
        OrdersCompleted++;
    }    
}
