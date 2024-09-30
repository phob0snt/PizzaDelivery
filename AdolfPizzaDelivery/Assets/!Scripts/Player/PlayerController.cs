using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private float _gravity = 9.81f;

    [SerializeField] private FixedJoystick _joystick;

    [SerializeField] private float _moveSpeedX;
    [SerializeField] private float _moveSpeedZ;

    [SerializeField] private Camera _cam;
    [SerializeField] private TouchCameraRotation _FPS;
    [SerializeField] private TouchCameraRotation _TPS;
    private TouchCameraRotation _currentCameraScript;

    private Animator _animator;   
    private CharacterController _controller;

    private float _moveSpeedXValue;
    private string _currAnimState;
    private float _yVel;
    #region consts
    private const string PLAYER_IDLE = "Player_Idle";
    private const string PLAYER_WALK = "Player_Walk";
    private const string PLAYER_RUN = "Player_Run";
    private const string PLAYER_WALK_BACKWARDS = "Player_Walk_Backwards";
    private const string PLAYER_WALK_LEFT = "Player_Walk_Left";
    private const string PLAYER_WALK_RIGHT = "Player_Walk_Right";
    #endregion

    private void Awake()
    {
        _currentCameraScript = _TPS;
        _moveSpeedXValue = _moveSpeedX;
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (OnGround())
            _yVel = 0;
        else
            _yVel -= _gravity * Time.deltaTime;

        _controller.Move(transform.forward * _joystick.Vertical * Time.deltaTime * _moveSpeedX + transform.right * _joystick.Horizontal * _moveSpeedZ * Time.deltaTime / 3 + new Vector3(0, _yVel, 0) * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + _currentCameraScript.YRotation, 0);
        _currentCameraScript.XCamRotation = transform.eulerAngles.y;


        if (_joystick.Vertical < 0)
            _moveSpeedX = _moveSpeedXValue / 3;
        else if (_joystick.Vertical > 0)
            _moveSpeedX = _moveSpeedXValue;

        HandleAnimations();
    }

    public void EnableFirstPersonView()
    {
        _TPS.gameObject.SetActive(false);
        _FPS.gameObject.SetActive(true);
        _currentCameraScript = _FPS;
    }

    public void EnableThirdPersonView()
    {
        _TPS.gameObject.SetActive(true);
        _FPS.gameObject.SetActive(false);
        _currentCameraScript = _TPS;
    }

    public void Teleport(Vector3 position)
    {
        _controller.enabled = false;
        transform.position = position;
        _controller.enabled = true;
    }

    private void HandleAnimations()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            if (Mathf.Abs(_joystick.Horizontal) < 0.5f)
            {
                if (_joystick.Vertical < 0)
                    ChangeAnimState(PLAYER_WALK_BACKWARDS);
                else if (_joystick.Vertical > 0.6f)
                    ChangeAnimState(PLAYER_RUN);
                else
                    ChangeAnimState(PLAYER_WALK);
            }
            else if (_joystick.Horizontal <= -0.5f)
            {
                ChangeAnimState(PLAYER_WALK_LEFT);
            }
            else if (_joystick.Horizontal >= 0.5f)
            {
                ChangeAnimState(PLAYER_WALK_RIGHT);
            }
        }
        else
            ChangeAnimState(PLAYER_IDLE);
    }

    private void ChangeAnimState(string state)
    {
        if (_currAnimState == state) return;
        _animator.Play(state);
        _animator.CrossFadeInFixedTime(state, 0.2f);
        _currAnimState = state;
    }

    private bool OnGround()
    {
        if (Physics.Raycast(transform.position, -transform.up, 0.01f))
            return true;
        else
            return false;
    }
}
