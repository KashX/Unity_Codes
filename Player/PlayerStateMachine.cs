using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public class PlayerStateMachine : MonoBehaviour
{
    PlayerBaseState currentState;
    PlayerStateFactory states;

    //_____________...getter and setter..._______________________________________________________________________________________
    public PlayerBaseState CurrentState {get {return currentState;} set {currentState = value;}}

    public PushObjCollectData PushScript {get {return pushScript;}}
    public Animator Animator {get {return animator;}}
    public CharacterController CharacterController {get {return characterController;}}
    public bool IsJumpPressed {get {return isJumpPressed;}}
    public int IsJumpingHash {get {return isJumpingHash;}}
    public int IsMovingHash {get {return isMovingHash;}}
    public int IsPushKickChargeHash {get {return isPushKickChargeHash;}}
    public int IsPushKickReleaseHash {get {return isPushKickReleaseHash;}}
    public Vector2 CurrentMovementInput {get {return currentMovementInput;}}
    public float CurrentMovementY {get {return currentMovement.y;} set{currentMovement.y = value;}}
    public float AppliedMovementY {get {return appliedMovement.y;} set{appliedMovement.y = value;}}
    public float AppliedMovementX {get {return appliedMovement.x;} set{appliedMovement.x = value;}}
    public float AppliedMovementZ {get {return appliedMovement.z;} set{appliedMovement.z = value;}}
    public bool IsMovementPressed {get {return isMovementPressed;}}
    public float MoveSpeed {get {return moveSpeed;} set {moveSpeed = value;}}
    public bool RequireNewJumpPress {get {return requireNewJumpPress;} set {requireNewJumpPress = value;}}
    public bool IsJumping {set {isJumping = value;}}
    public float InitialJumpVelocity {get {return initialJumpVelocity;}}
    public float Gravity {get {return gravity;} set {gravity = value;}}
    public float JumpGravity {get {return jumpGravity;} set {jumpGravity = value;}}

    public bool IsHold {get {return isHold;}}
    public bool IsPushKick {get {return isPushkick;}}
    public GameObject PushableObject {get {return pushableObject;} set {pushableObject = value;}}
    public Rigidbody RB {get {return rb;} set {rb = value;}}
    // .........for animations............
    public TwoBoneIKConstraint LeftHandIK {get {return leftHandIK;}}
    public TwoBoneIKConstraint RightHandIK {get {return rightHandIK;}}
    public Transform PushPosition {get {return pushPosition;}}
    public Transform PlayerTransform {get {return playerTransform;}}
    public bool ReadyToPush {get {return _readyToPush;} set {_readyToPush = value;}}
    public float Timer {get {return _timer;} set {_timer = value;}}


    //_____________...All Variables needed..._____________________________________________________________________________________
    private Player playerInput;
    private CharacterController characterController;
    private Animator animator;
    [SerializeField] private PushObjCollectData pushScript;

    //Variables to store optimized Animation setter/getter IDs
    int isMovingHash;
    int isJumpingHash;
    int isPushKickChargeHash;
    int isPushKickReleaseHash;

    //Variables to store player input values
    private Vector2 currentMovementInput;
    private Vector3 currentMovement;
    private Vector3 appliedMovement;
    private bool isMovementPressed;
    [SerializeField] private float rotationFactor;
    [SerializeField] private float moveSpeed = 1;

    private float gravity = -9.81f;
    private float jumpGravity;
    private bool isJumpPressed = false;
    private float initialJumpVelocity;
    [SerializeField]private float maxJumpHeight = 4.0f;
    private float maxJumpTime = 0.75f;
    private bool isJumping = false;
    private bool requireNewJumpPress = false;

    private bool isHold = false;
    private GameObject pushableObject;
    private Rigidbody rb;

    private bool isPushkick;
    private float _timer;

    // <.................................Animations...............................>
    private TwoBoneIKConstraint leftHandIK;
    private TwoBoneIKConstraint rightHandIK;
    private Transform pushPosition;
    private Transform playerTransform;
    [SerializeField] private bool _readyToPush;


    private void Awake()
    {
        playerTransform = this.transform;
        playerInput = new Player();
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        //
        isMovingHash = Animator.StringToHash("isMoving");
        isJumpingHash = Animator.StringToHash("isJumping");
        isPushKickChargeHash = Animator.StringToHash("isPushKickCharging");
        isPushKickReleaseHash = Animator.StringToHash("isPushKickReleasing");

        //set player input callbacks
        playerInput.PlayerMain.Move.started += OnMovementInput;
        playerInput.PlayerMain.Move.canceled += OnMovementInput;
        playerInput.PlayerMain.Move.performed += OnMovementInput;
        playerInput.PlayerMain.Jump.started += OnJump;
        playerInput.PlayerMain.Jump.canceled += OnJump;
        playerInput.PlayerMain.Hold.started += OnHold;
        playerInput.PlayerMain.Hold.canceled += OnHold;
        playerInput.PlayerMain.PushKick.started += OnPushKick;
        playerInput.PlayerMain.PushKick.canceled += OnPushKick;

        SetUpJumpVariables();

        //States Setup
        states = new PlayerStateFactory(this);
        currentState = states.Grounded();
        currentState.EnterState();
    }

    private void Start()
    {
        characterController.Move(appliedMovement * moveSpeed * Time.deltaTime);
    }

    private void Update()
    {
        HandleRotation();
        currentState.UpdateStates();

        characterController.Move(appliedMovement * moveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        
    }

    //<......................................Setting up Control Variables...................................................................>
    private void SetUpJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        float initialgravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight)/timeToApex;
        jumpGravity = initialgravity;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        requireNewJumpPress = false;
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    private void OnHold(InputAction.CallbackContext context)
    {
        isHold = context.ReadValueAsButton();
    }

    private void OnPushKick(InputAction.CallbackContext context)
    {
        isPushkick = context.ReadValueAsButton();
    }

    //<....................................Handle Movements and Rotations..............................................................>
    private void HandleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if(isMovementPressed && !isHold && !isPushkick)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactor*Time.deltaTime);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("ObjectToPush"))
        {
            rb = hit.collider.attachedRigidbody;
            pushScript = hit.gameObject.GetComponent<PushObjCollectData>();
            _readyToPush = pushScript.CanPush;
        }
        
    }

    private void OnEnable() 
    {
        playerInput.PlayerMain.Enable();    
    }
    private void OnDisable() 
    {
        playerInput.PlayerMain.Disable();
    }
}
