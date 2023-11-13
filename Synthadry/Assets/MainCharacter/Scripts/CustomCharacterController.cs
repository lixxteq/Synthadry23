
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
    
    public float jumpForce = 3.5f;
    public float walkingSpeed = 10f;
    public float runningSpeed = 15f;
    public float _currentSpeed = 0f;
    public float gravity = -9.8f;
    public float animationInterpolation = 1f;
    // Хранит движение игрока (x,y) в текущий фрейм
    private Vector2 _currentMovement = Vector2.zero;
    // Хранит rig.velocity в текущий фрейм
    public Vector3 _currentVelocity = Vector3.zero;
    public Vector2 _currentLook;
    private float xRotation = 0f;
    public float ySensitivity = 2f;
    public float xSensitivity = 2f;

    public float horisontal;
    public float vertical;
    private float lerpMulti = 7f;
    private TakeInHand takeInHand;

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
        takeInHand = this.GetComponent<TakeInHand>();
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
        _currentSpeed = walkingSpeed;
        IsMoving = _currentMovement.x != 0 || _currentMovement.y != 0;
    }

    private void OnLook(InputValue value) {
        _currentLook = value.Get<Vector2>();
    }

    void Run()
    {
        animationInterpolation = Mathf.Lerp(animationInterpolation, 1.5f, Time.deltaTime * 3);
        _anim.SetFloat("x", horisontal * animationInterpolation);
        _anim.SetFloat("y", vertical * animationInterpolation);

        _currentSpeed = Mathf.Lerp(_currentSpeed, runningSpeed, Time.deltaTime * 3);
        _anim.SetBool("isRunning", true);
        IsRunning = true;
    }
    public void Walk()
    {
        // Mathf.Lerp - ������� �� ��, ����� ������ ���� ����� animationInterpolation(� ������ ������) ������������ � ����� 1 �� ��������� Time.deltaTime * 3.
        // animationInterpolation = Mathf.Lerp(animationInterpolation, 1f, Time.deltaTime * 3);
        // _anim.SetFloat("x", horisontal * 0.25f);
        // _anim.SetFloat("y", vertical * 0.25f);

        //currentSpeed = Mathf.Lerp(currentSpeed, walkingSpeed, Time.deltaTime * 3);
        // currentSpeed = Mathf.Lerp(currentSpeed, walkingSpeed, Time.deltaTime * 3);
        // _anim.SetBool("isRunning", false);
        // IsRunning = false;
    }

    private void Update()
    {
        RotationController();
        CurrentState.UpdateState();
        // if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)) && Input.GetKey(KeyCode.LeftShift))
        // {

        //     Run();
        // }
        // else
        // {
        //     _anim.SetBool("RifleRunning", false);
        //     Walk();
        // }

        // if (_characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        // {
        //     _anim.SetTrigger("Jump");
        // }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        /*  // ����� �� ������ �������� ��������� � ����������� �� ����������� � ������� ������� ������
          // ��������� ����������� ������ � ������ �� ������ 
          Vector3 camF = mainCamera.forward;
          Vector3 camR = mainCamera.right;
          // ����� ����������� ������ � ������ �� �������� �� ���� ������� �� ������ ����� ��� ����, ����� ����� �� ������� ������, �������� ����� ���� ������� ��� ����� ������� ����� ��� ����
          // ������ ���� ��������� ��� ����� ����� camF.y = 0 � camR.y = 0 :)
          camF.y = 0;
          camR.y = 0;
          Vector3 movingVector;
          // ��� �� �������� ���� ������� �� ������ W & S �� ����������� ������ ������ � ���������� � �������� �� ������ A & D � �������� �� ����������� ������ ������
          movingVector = Vector3.ClampMagnitude(camF.normalized * vertical * currentSpeed + camR.normalized * horisontal * currentSpeed, currentSpeed);
          // Magnitude - ��� ������ �������. � ���� ������ �� currentSpeed ��� ��� �� �������� ���� ������ �� currentSpeed �� 86 ������. � ���� �������� ����� �������� 1.
          anim.SetFloat("magnitude", movingVector.magnitude / currentSpeed);
          //Debug.Log(movingVector.magnitude / currentSpeed);
          // ����� �� ������� ���������! ������������� �������� ������ �� x & z ������ ��� �� �� ����� ����� ��� �������� ������� � ������
          rig.velocity = new Vector3(movingVector.x, rig.velocity.y, movingVector.z);
          // � ���� ��� ���, ��� �������� �������� �� ����� � ��� �������� � ������� ���� ������
          rig.angularVelocity = Vector3.zero;*/


        // Vector3 camF = mainCamera.forward;
        // Vector3 camR = mainCamera.right;
        // camF.y = 0;
        // camR.y = 0;
        // Vector3 movingVector;
        // movingVector = Vector3.ClampMagnitude(camF.normalized * vertical * currentSpeed + camR.normalized * horisontal * currentSpeed, currentSpeed);
        // _anim.SetFloat("magnitude", movingVector.magnitude / currentSpeed);

        // if (!_characterController.isGrounded)
        // {
        //     movingVector.y -= walkingSpeed * 2;
        // }


        // _characterController.Move(movingVector * Time.fixedDeltaTime);
        CurrentState.FixedUpdateState();
    }

    private void LateUpdate() {
        RotationController();
    }
    public void Jump()
    {

        // rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // if (_characterController.isGrounded)
        // {
        //     _characterController.Move(Vector3.up * jumpForce * Time.fixedDeltaTime);
        // }
    }

    public bool IsRunning //����������
    {
        get { return _isRunning; }
        set
        {
            if (value)
            {
                takeInHand.SetIk(endWeight: 0);
                takeInHand.SetRunItemOffset();
            }
            else
            {
                takeInHand.SetIk(endWeight: 1);
                takeInHand.SetRunItemOffset(stopRunning: true);
            }
        }
    }

    private void RotationController()
    {
        // Ray desiredTargetRay = mainCamera.gameObject.GetComponent<Camera>().ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        // Vector3 desiredTargetPosition = desiredTargetRay.origin + desiredTargetRay.direction * multy;
        // aimTarget.position = Vector3.Lerp(aimTarget.position, desiredTargetPosition, aimLerp * Time.deltaTime);


        // // horisontal = Input.GetAxis("Horizontal") * animationInterpolation;
        // // vertical = Input.GetAxis("Vertical") * animationInterpolation;
        // horisontal = _currentMovement.x * animationInterpolation;
        // vertical = _currentMovement.y * animationInterpolation;

        // transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, mainCamera.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        xRotation -= (_currentLook.y * Time.deltaTime) * ySensitivity;
        xRotation = Math.Clamp(xRotation, -80f, 80f);

        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (_currentLook.x * Time.deltaTime) * xSensitivity);
    }
}