using UnityEngine;
using UnityEngine.Events;

public class Marker : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnMarkerInteract = new();
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            OnMarkerInteract.Invoke();
        }
    }
}
