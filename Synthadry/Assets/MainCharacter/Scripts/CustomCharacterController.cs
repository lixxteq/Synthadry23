using System;
using Unity.VisualScripting;
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
    private AbilityController _abil;

    public float cameraAngleUp = -65;
    public float cameraAngleDown = 65;


    public float jumpForce = 3.5f;
    public float maxForce = 10f;
    public float walkingSpeed = 10f;
    public float runningSpeed = 15f;
    public float acceleration = 3f;
    public float footRotation = 0;
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

    public bool _isRunning = false;
    public bool _isJumping = false;
    public bool _isMoving = false;
    private ItemsIK _itemsIK;
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
    public InputManager InputM { get {return _inputManager;}}

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _abil = GetComponent<AbilityController>();
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
        _itemsIK = this.GetComponent<ItemsIK>();
    }

    // Группа ивентов PlayerInput (вызываются при вводе игрока)
    public void OnJump(InputAction.CallbackContext ctx) {
        IsJumping = ctx.ReadValueAsButton();
    }

    public void OnMove(InputAction.CallbackContext ctx) {
        IsMoving = ctx.ReadValue<Vector2>().magnitude != 0f;
    }

    public void OnRun(InputAction.CallbackContext ctx) {
        IsRunning = ctx.ReadValueAsButton();
        // RifleIkHandler();
        Debug.Log("run triggered");
    }

    private void Update()
    {
        CurrentState.UpdateStates();
        RotationController();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CurrentState.FixedUpdateStates();
    }

    private void LateUpdate() {
        // RotationController();
    }

    // Поворачивает transform игрока при движении мыши
    // private void RotationController()
    // {
    //     rotationAcc += _inputManager.GetCurrentLook().x;
    //     // transform.Rotate(Vector3.up * (_inputManager.GetCurrentLook().x * Time.deltaTime) * xSensitivity);
    //     if (rotationAcc >= 90 | rotationAcc <= -90) {
    //         transform.Rotate(Vector3.up * (rotationAcc * Time.deltaTime) * xSensitivity);
    //         rotationAcc = 0;
    //         Animator.SetFloat("rotation", rotationAcc >= 90 ? 1 : -1);
    //     }
        
    // }
    private void RotationController() {
        float mouseX = Input.GetAxis("Mouse X");
        footRotation = Mathf.Lerp(footRotation, mouseX, 5 * Time.deltaTime);
        Animator.SetFloat("rotation", footRotation);
        transform.Rotate(Vector3.up * (_inputManager.GetCurrentLook().x * Time.deltaTime) * xSensitivity);
    }

    private void RifleIkHandler() {
        _itemsIK.SetWeaponIKRig(weight: IsRunning ? 0 : 1);
        // _itemsIK.SetRunItemOffset(stopRunning: !IsRunning);
    }
}