using UnityEngine;
using Zenject;

public class DeliveryMarker : Marker
{
    [HideInInspector] public DeliveryOrder OrderRef;
    [HideInInspector] public Transform PizzaPos;
    [Inject] private DestinationArrow _minimapArrow;

    protected override void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.GetComponent<Player>() != null)
        {
            other.GetComponent<Player>().PutPizza(PizzaPos);
            OrderRef.OrderChain.CompleteOrder(OrderRef);
            _minimapArrow.Markers.Remove(transform);
            gameObject.SetActive(false);
        }
    }
}
