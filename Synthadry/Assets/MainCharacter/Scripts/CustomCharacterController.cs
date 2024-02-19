using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomCharacterController : MonoBehaviour
{
    [SerializeField] private Transform aimTarget;
    public float aimLerp;
    public float multy;

    public Canvas _canvas;
    public Animator _animator;
    public Rigidbody rig;

    public float cameraAngleUp = -65;
    public float cameraAngleDown = 65;


    public float jumpForce = 3.5f;
    public float maxForce = 10f;
    public float walkingSpeed = 10f;
    public float runningSpeed = 15f;
    public float _currentSpeed = 0f;
    public float gravity = -9.8f;
    public float animationInterpolation = 1f;
    // Хранит движение игрока (x,y) перед прыжком
    public Vector2 _appliedMovement = Vector2.zero;
    // Хранит rig.velocity в текущий фрейм
    public Vector3 _currentVelocity = Vector3.zero;
    public float ySensitivity = 2f;
    public float xSensitivity = 2f;
    private float lerpMulti = 7f;
    // private TakeInHand takeInHand;

    public bool _isRunning = false;
    public bool _isJumping = false;
    public bool _isMoving = false;
    private CharacterController _characterController;
    private InputManager _inputManager;
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;

    // player state related set / get methods
    public PlayerBaseState CurrentState { get {return _currentState;} set {_currentState = value;}}
    public CharacterController CharacterController { get {return _characterController;}}
    public Animator Animator { get {return _animator;}}
    public bool IsJumping { get {return _isJumping;} set {_isJumping = value;}}
    public bool IsMoving { get {return _isMoving;} set {_isMoving = value;}}
    public bool IsRunning { get {return _isRunning;} set {_isRunning = value;}}
    public InputManager Input { get {return _inputManager;}}

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _states = new PlayerStateFactory(this);

        _currentState = _states.Grounded();
        _currentState.EnterState();
    }

    void Start()
    {
        _inputManager = InputManager.Instance;
        _canvas = FindObjectOfType<Canvas>();
        // this.GetComponent<CustomCharacterController>().enabled = false;
        // this.GetComponent<CustomCharacterController>().enabled = true;
        // takeInHand = this.GetComponent<TakeInHand>();
    }

    // Группа ивентов PlayerInput (вызываются при вводе игрока)
    // private void OnEnable()
    // {
    //     _playerInput.ActivateInput();
    // }

    // private void OnJump(InputValue value) {
    //     IsJumping = value.isPressed;
    // }

    // private void OnMove(InputValue value) {
    //     // _currentMovement = value.Get<Vector2>();
    //     // _currentSpeed = walkingSpeed;
    //     // IsMoving = _currentMovement.x != 0 || _currentMovement.y != 0;
    //     IsMoving = value.Get<Vector2>().magnitude == 0f;
    // }

    // private void OnRun(InputValue value) {
    //     IsRunning = Convert.ToBoolean(value.Get<float>());
    //     RifleIkHandler();
    //     Debug.Log("run triggered");
    // }
    public void OnJump(InputAction.CallbackContext ctx) {
        IsJumping = ctx.ReadValueAsButton();
    }

    public void OnMove(InputAction.CallbackContext ctx) {
        // _currentMovement = value.Get<Vector2>();
        // _currentSpeed = walkingSpeed;
        // IsMoving = _currentMovement.x != 0 || _currentMovement.y != 0;
        IsMoving = ctx.ReadValue<Vector2>().magnitude != 0f;
    }

    public void OnRun(InputAction.CallbackContext ctx) {
        IsRunning = ctx.ReadValueAsButton();
        RifleIkHandler();
        Debug.Log("run triggered");
    }

    private void Update()
    {
        CurrentState.UpdateStates();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CurrentState.FixedUpdateStates();
    }

    private void LateUpdate() {
        RotationController();
    }


    // public bool IsRunning //����������
    // {
    //     get { return _isRunning; }
    //     set
    //     {
    //         if (value)
    //         {
    //             // takeInHand.SetIk(endWeight: 0);
    //             // takeInHand.SetRunItemOffset();
    //         }
    //         else
    //         {
    //             // takeInHand.SetIk(endWeight: 1);
    //             // takeInHand.SetRunItemOffset(stopRunning: true);
    //         }
    //         _isRunning = value;
    //     }
    // }

    // Поворачивает transform игрока при движении мыши
    private void RotationController()
    {
        transform.Rotate(Vector3.up * (_inputManager.GetCurrentLook().x * Time.deltaTime) * xSensitivity);
    }

    private void RifleIkHandler() {
        // takeInHand.SetIk(endWeight: IsRunning ? 0 : 1);
        // takeInHand.SetRunItemOffset(stopRunning: IsRunning);
    }
}