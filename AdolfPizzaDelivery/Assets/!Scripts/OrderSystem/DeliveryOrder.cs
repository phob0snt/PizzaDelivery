using UnityEngine;

public class DeliveryOrder : ScriptableObject
{
    public DeliveryPoint Destination;
    public OrderChain OrderChain;

    public void CompleteOrder()
    {
        DeliveryManager.OnCompletedOrder.Invoke();
        Destroy(this);
    }

    public void ConfigureOrder()
    {
        Destination.CreateMarker(this);
    }
}
