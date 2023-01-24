using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBasic : MonoBehaviour
{
    //reference variables
    private Player playerInput;
    private CharacterController characterController;
    private Animator animator;

    //Variables to store optimized Animation setter/getter IDs
    int isMovingHash;
    int isJumpingHash;

    //Variables to store player input values
    private Vector2 currentMovementInput;
    private Vector3 currentMovement;
    private Vector3 appliedMovement;
    private bool isMovementPressed;
    [SerializeField] private float rotationFactor;
    [SerializeField] private float moveSpeed = 1;

    private float groundedGravity = -0.05f;
    private float gravity = -9.81f;

    private bool isJumpPressed = false;
    private float initialJumpVelocity;
    [SerializeField]private float maxJumpHeight = 4.0f;
    private float maxJumpTime = 0.75f;
    private bool isJumping = false;
    private bool isJumpAnimating;

    private void Awake() 
    {
        playerInput = new Player();
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        //
        isMovingHash = Animator.StringToHash("isMoving");
        isJumpingHash = Animator.StringToHash("isJumping");

        //set player input callbacks
        playerInput.PlayerMain.Move.started += OnMovementInput;
        playerInput.PlayerMain.Move.canceled += OnMovementInput;
        playerInput.PlayerMain.Move.performed += OnMovementInput;
        playerInput.PlayerMain.Jump.started += OnJump;
        playerInput.PlayerMain.Jump.canceled += OnJump;

        SetUpJumpVariables();
    }

    private void SetUpJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight)/timeToApex;
    }

    private void HandleJump()
    {
        if(!isJumping && characterController.isGrounded && isJumpPressed)
        {
            animator.SetBool(isJumpingHash, true);
            isJumpAnimating = true;
            isJumping = true;
            currentMovement.y = initialJumpVelocity;
            appliedMovement.y = initialJumpVelocity;
        }
        else if(!isJumpPressed && characterController.isGrounded && isJumping)
        {
            isJumping = false;
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    private void HandleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if(isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactor*Time.deltaTime);
        }
    }

    private void HandleGravity()
    {
        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        float fallMultiplier = 2.0f;

        if(characterController.isGrounded)
        {
            if(isJumpAnimating)
            {
                animator.SetBool(isJumpingHash, false);
                isJumpAnimating = false;
            }
            currentMovement.y = groundedGravity;
            appliedMovement.y = groundedGravity;
        }
        else if(isFalling)
        {
            float previousYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y +(gravity * fallMultiplier * Time.deltaTime);
            appliedMovement.y = Mathf.Max((previousYVelocity + currentMovement.y) * 0.5f, -20.0f);
        }
        else
        {
            float previousYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y + (gravity * Time.deltaTime);
            appliedMovement.y = (previousYVelocity + currentMovement.y) * 0.5f;
        }
    }

    private void HandleAnimation()
    {
        bool isMoving = animator.GetBool(isMovingHash);
        bool isJumping = animator.GetBool(isJumpingHash);

        if(isMovementPressed && !isMoving)
        {
            animator.SetBool(isMovingHash, true);
        }
        else if(!isMovementPressed && isMoving)
        {
            animator.SetBool(isMovingHash, false);
        }
    }
    void Update()
    {
        HandleRotation();
        HandleAnimation();

        appliedMovement.x = currentMovement.x;
        appliedMovement.z = currentMovement.z;
        characterController.Move(appliedMovement * moveSpeed * Time.deltaTime);

        HandleGravity();
        HandleJump();
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
