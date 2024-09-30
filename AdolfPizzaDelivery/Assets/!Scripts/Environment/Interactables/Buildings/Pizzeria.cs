using UnityEngine;
using Zenject;

public class Pizzeria : MonoBehaviour
{
    [Inject] private readonly ViewManager _viewManager;
    [SerializeField] private Marker _pizzeriaMarker;

    private void OnEnable()
    {
        _pizzeriaMarker.OnMarkerInteract.AddListener(() => _viewManager.Show<PizzeriaView>(true, false));
    }

    private void OnDisable()
    {
        _pizzeriaMarker.OnMarkerInteract.AddListener(() => _viewManager.Show<PizzeriaView>(true, false));
    }
}
