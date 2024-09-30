using System.Collections;
using UnityEngine;
using Zenject;

public class Garage : MonoBehaviour
{
    [Inject] private Player _player;
    [SerializeField] private Marker _enterMarker;
    [SerializeField] private Marker _exitMarker;
    [SerializeField] private Transform _playerEnterPoint;
    [SerializeField] private Transform _playerExitPoint;

    private void OnEnable()
    {
        _enterMarker.OnMarkerInteract.AddListener(EnterGarage);
        _exitMarker.OnMarkerInteract.AddListener(ExitGarage);
    }

    private void OnDisable()
    {
        _enterMarker.OnMarkerInteract.RemoveListener(EnterGarage);
        _exitMarker.OnMarkerInteract.RemoveListener(ExitGarage);
    }
    private void EnterGarage()
    {
        StartCoroutine(EnterTransiction());
    }
    private void ExitGarage()
    {
        StartCoroutine(ExitTransiction());
    }

    private IEnumerator EnterTransiction()
    {
        _player.DarkenScreen(1.4f);
        yield return new WaitForSeconds(0.9f);
        _player.PlayerController.EnableFirstPersonView();
        _player.PlayerController.Teleport(_playerEnterPoint.position);
    }

    private IEnumerator ExitTransiction()
    {
        _player.DarkenScreen(1.4f);
        yield return new WaitForSeconds(0.9f);
        _player.PlayerController.EnableThirdPersonView();
        _player.PlayerController.Teleport(_playerExitPoint.position);
    }


}
