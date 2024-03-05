using UnityEngine;
using Zenject;

public class PizzeriaMarker : MonoBehaviour
{
    [Inject] private ViewManager _viewManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _viewManager.Show<PizzeriaView>(true, false);
        }
    }
}
