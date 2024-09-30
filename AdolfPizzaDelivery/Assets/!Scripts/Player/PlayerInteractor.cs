using UnityEngine;
using Zenject;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private Transform _cam;

    private PlayerView _playerView;
    [Inject] private ViewManager _viewManager;

    private void Awake()
    {
        _playerView = _viewManager.GetView<PlayerView>();
    }
    private void Update()
    {
        GetInteraction();
    }

    private void GetInteraction()
    {
        RaycastHit hit;
        Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), _cam.transform.forward, out hit, 1.75f);
        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<Mushroom>() != null)
                _playerView.ToggleUseBtn(true, hit.collider.GetComponent<IUsable>());
            else
                _playerView.ToggleUseBtn(false);
        }
        else
            _playerView.ToggleUseBtn(false);

    }

    
}
