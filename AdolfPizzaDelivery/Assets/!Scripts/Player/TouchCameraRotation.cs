using Cinemachine;
using UnityEngine;

public class TouchCameraRotation : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook _virtualCamera;
    [SerializeField] private FloatingJoystick _joystick;
    public float Sensivity;
    private const int YAXIS_SENS_COEFF = 40;
    public float XCamRotation;
    public float YCamRotation;
    private float _prevYRotation;
    private const int MAX_XROTATION_DEGREE_PER_ONE_SLIDE = 60;
    private float _currentDegree = 0;

    private void Awake()
    {
        SettingsManager.Instance.CameraRotation = this;
    }

    void FixedUpdate()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _virtualCamera.m_BindingMode = CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;
            _prevYRotation = _virtualCamera.m_YAxis.Value;
            XCamRotation = _joystick.Horizontal * Sensivity;
            YCamRotation = _joystick.Vertical * (Sensivity / YAXIS_SENS_COEFF);
            _currentDegree += XCamRotation;
            if (_currentDegree > MAX_XROTATION_DEGREE_PER_ONE_SLIDE)
                _currentDegree = MAX_XROTATION_DEGREE_PER_ONE_SLIDE;
            else if (_currentDegree < -MAX_XROTATION_DEGREE_PER_ONE_SLIDE)
                _currentDegree = -MAX_XROTATION_DEGREE_PER_ONE_SLIDE;
            if (_currentDegree < MAX_XROTATION_DEGREE_PER_ONE_SLIDE && XCamRotation > 0)
                _virtualCamera.m_XAxis.Value = XCamRotation;
            else if (_currentDegree > -MAX_XROTATION_DEGREE_PER_ONE_SLIDE && XCamRotation < 0)
                _virtualCamera.m_XAxis.Value = XCamRotation;
            _virtualCamera.m_YAxis.Value += YCamRotation;
        }     
        else
        {
            _currentDegree = 0;
            _virtualCamera.m_BindingMode = CinemachineTransposer.BindingMode.LockToTarget;
            _virtualCamera.m_YAxis.Value = _prevYRotation;
        }  
        
    }
}
