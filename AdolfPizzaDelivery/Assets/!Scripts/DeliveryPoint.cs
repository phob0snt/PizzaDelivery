using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DeliveryPoint : MonoBehaviour
{
    public GameObject pointMarker;
    [SerializeField] private Transform pizzaPlacement;
    [Inject] private DestinationArrow minimapArrow;

    public void CreateMarker(DeliveryOrder orderRef)
    {
        minimapArrow.markerPos = pointMarker.transform;
        pointMarker.SetActive(true);
        pointMarker.GetComponent<DeliveryMarker>().OrderRef = orderRef;
        pointMarker.GetComponent<DeliveryMarker>().PizzaPos = pizzaPlacement;
    }
}
