using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleFloat : MonoBehaviour
{
    private Player playerInput;

    [SerializeField]
    private Vector3 moveDirection;
    [SerializeField]
    private float speed;
    
    [SerializeField]
    private Collider col;

    void Start()
    {
        col = GetComponent<Collider>();
        playerInput = new Player();
    }

    void FixedUpdate()
    {
        transform.Translate(moveDirection, Space.Self);

        if (playerInput.PlayerMain.Jump.triggered)
        {
            col.enabled = false;
        }
    }
}