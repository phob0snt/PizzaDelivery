using Cinemachine;
using UnityEngine;

public class TouchCameraRotation : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    private CinemachinePOV _pov;
    [SerializeField] private FloatingJoystick _joystick;
    private const int YAXIS_SENS_COEFF = 25;
    public float Sensivity;
    public float XCamRotation
    {
        get { return _xCamRotation; }
        set {
            _pov.m_HorizontalAxis.Value = value;
        }
    }
    private float _xCamRotation;
    private bool _canRotate;
    public float YRotation;
    private Touch _rotationTouch;

    private void Awake()
    {
        SettingsManager.Instance.SetCameraRotation(this);
    }

    private void Start()
    {
        _pov = _virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        XCamRotation = _pov.m_HorizontalAxis.Value;
    }

    private void Update()
    {
        if (Input.touchCount == 0)
        {
            YRotation = 0;
            _rotationTouch = new Touch();
        }
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).position.x > Screen.width / 2)
            {
                _rotationTouch = Input.GetTouch(i);
                YRotation = _rotationTouch.deltaPosition.x * Time.deltaTime * Sensivity;
                _pov.m_VerticalAxis.Value -= _rotationTouch.deltaPosition.y * Time.deltaTime * Sensivity;
            }
            else if (Input.GetTouch(i).fingerId == _rotationTouch.fingerId || _rotationTouch.phase == TouchPhase.Ended)
            {
                _rotationTouch = new Touch();
                YRotation = 0;
            }
        }
    }
}
