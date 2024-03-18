 using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;


public class PlayerMovement : NetworkBehaviour
{
    //PlayerInput Shit
    PlayerInput pInput;
    PlayerManager pManager;
    Rigidbody rb;


    //VARS
    private float defaultSpeed;


    [SerializeField] Transform camTransform;

    private void Awake()
    {
        pInput = GetComponent<PlayerInput>();
        pManager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pInput.actions["Sprint"].started += PlayerMovement_started;
        pInput.actions["Sprint"].canceled += PlayerMovement_canceled;
        defaultSpeed = pManager.playerSpeed;
    }

    private void PlayerMovement_canceled(InputAction.CallbackContext obj)
    {
        pManager.playerSprint = false;
        pManager.playerSpeed = defaultSpeed;
    }

    private void PlayerMovement_started(InputAction.CallbackContext obj)
    {
        pManager.playerSprint = true;
        pManager.playerSpeed = defaultSpeed * 1.5f;
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        Movement();
    }

    private void Movement()
    {
        Vector3 movementDirection = camTransform.transform.TransformDirection(new Vector3(Inputs().x, 0, Inputs().y));
        pManager.playerCurrentInputs = Inputs();

        rb.velocity = new Vector3(movementDirection.x * pManager.playerSpeed, rb.velocity.y, movementDirection.z * pManager.playerSpeed);
    }


    private Vector2 Inputs()
    {
        return pInput.actions["Movement"].ReadValue<Vector2>();
        
    }
}