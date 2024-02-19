using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryMarker : MonoBehaviour
{
    [HideInInspector] public DeliveryOrder OrderRef;
    [HideInInspector] public Transform PizzaPos;
    private bool isDropped = false;
    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag("Player") && !isDropped)
        {
            isDropped = true;
            other.GetComponent<Player>().PutPizza(PizzaPos);
            OrderRef.CompleteOrder();
            Destroy(gameObject);
        }
    }
}
