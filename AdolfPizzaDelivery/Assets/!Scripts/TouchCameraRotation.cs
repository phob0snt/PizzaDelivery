using Cinemachine;
using UnityEngine;

public class TouchCameraRotation : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook _virtualCamera;
    [SerializeField] private FloatingJoystick _joystick;
    public float sensivity;
    private const int YAXIS_SENS_COEFF = 40;
    public float xCamRotation;
    public float yCamRotation;
    private float prevYRotation;
    private const int MAX_XROTATION_DEGREE_PER_ONE_SLIDE = 60;
    private float currentDegree = 0;

    private void Awake()
    {
        SettingsManager.Instance.CameraRotation = this;
    }

    void FixedUpdate()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _virtualCamera.m_BindingMode = CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;
            prevYRotation = _virtualCamera.m_YAxis.Value;
            xCamRotation = _joystick.Horizontal * sensivity;
            yCamRotation = _joystick.Vertical * (sensivity / YAXIS_SENS_COEFF);
            //if (yCamRotation >= 0 && yCamRotation <= 2)
            //{
            //    yCamRotation = _joystick.Vertical * (sensivity / YAXIS_SENS_COEFF);
            //}
            //else if (yCamRotation < 0)
            //{
            //    if (_joystick.Vertical > 0)
            //        yCamRotation = _joystick.Vertical * (sensivity / YAXIS_SENS_COEFF);
            //}
            //else if (yCamRotation > 2)
            //{
            //    if (_joystick.Vertical < 0)
            //        yCamRotation = _joystick.Vertical * (sensivity / YAXIS_SENS_COEFF);
            //}
            currentDegree += xCamRotation;
            if (currentDegree > MAX_XROTATION_DEGREE_PER_ONE_SLIDE)
                currentDegree = MAX_XROTATION_DEGREE_PER_ONE_SLIDE;
            else if (currentDegree < -MAX_XROTATION_DEGREE_PER_ONE_SLIDE)
                currentDegree = -MAX_XROTATION_DEGREE_PER_ONE_SLIDE;
            if (currentDegree < MAX_XROTATION_DEGREE_PER_ONE_SLIDE && xCamRotation > 0)
                _virtualCamera.m_XAxis.Value = xCamRotation;
            else if (currentDegree > -MAX_XROTATION_DEGREE_PER_ONE_SLIDE && xCamRotation < 0)
                _virtualCamera.m_XAxis.Value = xCamRotation;
            _virtualCamera.m_YAxis.Value += yCamRotation;
        }     
        else
        {
            currentDegree = 0;
            _virtualCamera.m_BindingMode = CinemachineTransposer.BindingMode.LockToTarget;
            _virtualCamera.m_YAxis.Value = prevYRotation;
        }  
        
    }
}
