using UnityEngine;

public class DeliveryOrder : ScriptableObject
{
    public DeliveryPoint Destination;
    public OrderChain OrderChain;

    public void Configure()
    {
        Destination.CreateMarker(this);
    }
}
