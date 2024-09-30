using UnityEngine;
using Zenject;

public class DeliveryPoint : MonoBehaviour
{
    public GameObject pointMarker;
    [SerializeField] private Transform _pizzaPlacement;
    [Inject] private DestinationArrow _minimapArrow;

    public int PointNumber { get; private set; }

    public void SetPointNumber(int pointNum)
    {
        PointNumber = pointNum;
    }

    public void CreateMarker(DeliveryOrder orderRef)
    {
        _minimapArrow.Markers.Add(pointMarker.transform);
        pointMarker.SetActive(true);
        pointMarker.GetComponent<DeliveryMarker>().OrderRef = orderRef;
        pointMarker.GetComponent<DeliveryMarker>().PizzaPos = _pizzaPlacement;
    }
}
