using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;
    private Animator animator;   
    [SerializeField] private float moveSpeedX;
    [SerializeField] private float moveSpeedZ;
    [SerializeField] private Camera cam;
    [SerializeField] private TouchCameraRotation cineTouch;
    [SerializeField] private float stepHeight;
    private CharacterController controller;
    private float moveSpeedXValue;
    private string currAnimState;
    [SerializeField] private RuntimeAnimatorController animatorController;
    private int canWalk = 1;
    private float yVel;
    #region consts
    private const string PLAYER_IDLE = "Player_Idle";
    private const string PLAYER_WALK = "Player_Walk";
    private const string PLAYER_RUN = "Player_Run";
    private const string PLAYER_WALK_BACKWARDS = "Player_Walk_Backwards";
    private const string PLAYER_WALK_LEFT = "Player_Walk_Left";
    private const string PLAYER_WALK_RIGHT = "Player_Walk_Right";
    #endregion

    void Awake()
    {
        moveSpeedXValue = moveSpeedX;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (OnGround())
            yVel = 0;
        else
            yVel -= 9.8f * Time.deltaTime;

        controller.Move(transform.forward * joystick.Vertical * Time.deltaTime * moveSpeedX + transform.right * joystick.Horizontal * moveSpeedZ * Time.deltaTime / 3 + new Vector3(0, yVel, 0) * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, cam.transform.eulerAngles.y, 0);

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            if (joystick.Vertical < 0)
                moveSpeedX = moveSpeedXValue / 3;
            else if (joystick.Vertical > 0)
                moveSpeedX = moveSpeedXValue;
            if (Mathf.Abs(joystick.Horizontal) < 0.5f)
            {
                if (joystick.Vertical < 0)
                    ChangeAnimState(PLAYER_WALK_BACKWARDS);
                else if (joystick.Vertical > 0.6f)
                    ChangeAnimState(PLAYER_RUN);
                else
                    ChangeAnimState(PLAYER_WALK);
            }
            else if (joystick.Horizontal <= -0.5f)
            {
                ChangeAnimState(PLAYER_WALK_LEFT);
            }
            else if (joystick.Horizontal >= 0.5f)
            {
                ChangeAnimState(PLAYER_WALK_RIGHT);
            }
        }
        else
            ChangeAnimState(PLAYER_IDLE);
    }

    private void ChangeAnimState(string state)
    {
        if (currAnimState == state) return;
        animator.Play(state);
        animator.CrossFadeInFixedTime(state, 0.2f);
        currAnimState = state;
    }

    private bool OnGround()
    {
        if (Physics.Raycast(transform.position, -transform.up, 0.01f))
            return true;
        else
            return false;
    }
}
