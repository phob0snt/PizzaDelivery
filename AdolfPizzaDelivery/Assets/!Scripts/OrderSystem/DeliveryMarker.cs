using UnityEngine;
using Zenject;

public class DeliveryMarker : MonoBehaviour
{
    [HideInInspector] public DeliveryOrder OrderRef;
    [HideInInspector] public Transform PizzaPos;
    [Inject] private DestinationArrow _minimapArrow;
    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.GetComponent<Player>() != null)
        {
            other.GetComponent<Player>().PutPizza(PizzaPos);
            OrderRef.CompleteOrder();
            OrderRef.OrderChain.OrdersCompleted++;
            _minimapArrow.Markers.Remove(transform);
            gameObject.SetActive(false);
        }
    }
}
