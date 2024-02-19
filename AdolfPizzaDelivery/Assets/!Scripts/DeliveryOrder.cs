using UnityEngine;

public class DeliveryOrder : ScriptableObject
{
    public DeliveryPoint Destination;
    public bool isCompleted;
    public OrderChain orderChain;

    public void CompleteOrder()
    {
        isCompleted = true;
        DeliveryManager.OnCompletedOrder.Invoke(Random.Range(8, 20), Random.Range(2, 5));
        Destroy(this);
    }

    public void ConfigureOrder()
    {
        Destination.CreateMarker(this);
    }
}
