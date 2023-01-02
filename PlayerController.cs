using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;

    private Player playerInput;
    private CharacterController controller;

    private Animator anime;
    [SerializeField]
    private GameObject player;
    
    private void Awake()
    {
        playerInput = new Player();
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        anime = player.GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            anime.SetBool("isJumping", false);
        }

        Vector2 movementInput = playerInput.PlayerMain.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0f, movementInput.y).normalized;
          
        //Camera Rotation
        move = move.x * cameraTransform.right + move.z * cameraTransform.forward;
        move.y = 0f;

        controller.Move(move * Time.deltaTime * playerSpeed);
        if (move != Vector3.zero)
        {
            anime.SetBool("isMoving", true);
            gameObject.transform.forward = move;
        }
        else
        {
            anime.SetBool("isMoving", false);
        }

        // Jump
        if (playerInput.PlayerMain.Jump.triggered && groundedPlayer)
        {
            Jump();
            
        }
        if (groundedPlayer)
        {
            anime.SetBool("isJumping", false);
        }
        else
        {
            anime.SetBool("isJumping", true);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void Jump()
    {        
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

    }

}
