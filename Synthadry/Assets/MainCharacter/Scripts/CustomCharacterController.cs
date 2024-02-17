
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomCharacterController : MonoBehaviour
{
    [SerializeField] private Transform aimTarget;
    public float aimLerp;
    public float multy;

    public Canvas canvas;

    public Animator _anim;
    public Rigidbody rig;
    public Transform mainCamera;

    public float cameraAngleUp = -65;
    public float cameraAngleDown = 65;


    public float jumpForce = 3.5f;
    public float maxForce = 10f;
    public float walkingSpeed = 10f;
    public float runningSpeed = 15f;
    public float _currentSpeed = 0f;
    public float gravity = -9.8f;
    public float animationInterpolation = 1f;
    // Хранит движение игрока (x,y) в текущий фрейм
    private Vector2 _currentMovement = Vector2.zero;
    // Хранит движение игрока (x,y) перед прыжком
    public Vector2 _appliedMovement = Vector2.zero;
    // Хранит rig.velocity в текущий фрейм
    public Vector3 _currentVelocity = Vector3.zero;
    public Vector2 _currentLook;
    private float xRotation = 0f;
    public float ySensitivity = 2f;
    public float xSensitivity = 2f;

    public float horisontal;
    public float vertical;
    private float lerpMulti = 7f;
    // private TakeInHand takeInHand;

    public bool canGo = true;
    public bool _isRunning = false;
    public bool _isJumping = false;
    public bool _isMoving = false;
    private CharacterController _characterController;
    private PlayerInput _playerInput;
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;

    // player state related set / get methods
    public PlayerBaseState CurrentState { get {return _currentState;} set {_currentState = value;}}
    public CharacterController CharacterController { get {return _characterController;}}
    public Animator Animator { get {return _anim;}}
    public Vector2 CurrentMovement { get {return _currentMovement;}}
    public bool IsJumping { get {return _isJumping;} set {_isJumping = value;}}
    public bool IsMoving { get {return _isMoving;} set {_isMoving = value;}}
    // public bool IsRunning { get {return _isRunning;}}

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _characterController = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
        _states = new PlayerStateFactory(this);

        _currentState = _states.Grounded();
        _currentState.EnterState();
    }

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        this.GetComponent<CustomCharacterController>().enabled = false;
        this.GetComponent<CustomCharacterController>().enabled = true;
        // takeInHand = this.GetComponent<TakeInHand>();
    }

    // Группа методов PlayerInput (вызываются при вводе игрока)
    private void OnEnable()
    {
        _playerInput.ActivateInput();
    }

    private void OnJump(InputValue value) {
        IsJumping = value.isPressed;
        Debug.Log("jump pressed");
    }

    private void OnMove(InputValue value) {
        _currentMovement = value.Get<Vector2>();
        // _currentSpeed = walkingSpeed;
        IsMoving = _currentMovement.x != 0 || _currentMovement.y != 0;
    }

    private void OnLook(InputValue value) {
        _currentLook = value.Get<Vector2>();
    }

    private void OnRun(InputValue value) {
        IsRunning = value.isPressed;
        Debug.Log("run pressed");
    }



    private void Update()
    {
        // RotationController();
        CurrentState.UpdateStates();

        // temporary workaround
        IsRunning = Input.GetKey(KeyCode.LeftShift);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CurrentState.FixedUpdateStates();
    }

    private void LateUpdate() {
        RotationController();
    }


    public bool IsRunning //����������
    {
        get { return _isRunning; }
        set
        {
            if (value)
            {
                // takeInHand.SetIk(endWeight: 0);
                // takeInHand.SetRunItemOffset();
            }
            else
            {
                // takeInHand.SetIk(endWeight: 1);
                // takeInHand.SetRunItemOffset(stopRunning: true);
            }
            _isRunning = value;
        }
    }

    private void RotationController()
    {
        xRotation -= _currentLook.y * Time.deltaTime * ySensitivity;
        xRotation = Math.Clamp(xRotation, cameraAngleUp, cameraAngleDown);

        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (_currentLook.x * Time.deltaTime) * xSensitivity);
    }
}