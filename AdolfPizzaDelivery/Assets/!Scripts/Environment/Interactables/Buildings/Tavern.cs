using UnityEngine;
using Zenject;

public class Tavern : MonoBehaviour
{
    [SerializeField] private Marker _tavernMarker;
    [Inject] private ViewManager _viewManager;

    private void OnEnable()
    {
        _tavernMarker.OnMarkerInteract.AddListener(EnterTavern);
    }

    private void EnterTavern()
    {
        _viewManager.Show<TavernView>(true, false);
    }
}
