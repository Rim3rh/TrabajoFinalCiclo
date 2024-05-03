 using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;


public class PlayerMovement : NetworkBehaviour
{
    #region Vars
    //class references
    PlayerManager pManager;
    //Componet References
    PlayerInput pInput;
    Rigidbody rb;
    //Private vars
    private float defaultSpeed;
    [SerializeField] bool hittingSprintButton;
    #endregion
    #region SelfRunningMethods
    private void Awake()
    {
        //getting components
        pInput = GetComponent<PlayerInput>();
        pManager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        if (!IsOwner) return;
        //this is so the cursor is inmvisible and it does not exit the game screen.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //subscribe to the player input events
        pInput.actions["Sprint"].started += PlayerSprint_Started;
        pInput.actions["Sprint"].canceled += PlayerSprint_canceled;
        //set default speed to the playerspeed
        defaultSpeed = pManager.playerSpeed;
    }
    private void PlayerSprint_canceled(InputAction.CallbackContext obj)
    {
        CancelSprint();
    }

    private void PlayerSprint_Started(InputAction.CallbackContext obj)
    {
        hittingSprintButton = true;
    }
    private void FixedUpdate()
    {
        if (!IsOwner) return;
        Movement();
    }

    #endregion
    #region private Methods

    private void Movement()
    {
        //Calculate the movedirecction vector based on the inputs
        Vector3 movementDirection = transform.TransformDirection(new Vector3(Inputs().x, 0, Inputs().y));
        //Give the playerManager the current inputs
        pManager.playerCurrentInputs = Inputs();
        //Aplly the speèd(not on the y)
        rb.velocity = new Vector3(movementDirection.x * pManager.playerSpeed, rb.velocity.y, movementDirection.z * pManager.playerSpeed);
        //if normal walking, speed is defaulted
        if ((Inputs().y > 0 || Mathf.Abs(Inputs().x) > 0 ) && !pManager.playerSprint)
        {
            pManager.playerSpeed = defaultSpeed;
        }
        //if walking backwads, walk slower
        if (Inputs().y < 0)
        {
            pManager.playerSprint = false;
            pManager.playerSpeed = defaultSpeed / 2;
        }

        //Sprint
        if (hittingSprintButton)
        {
            //if walking, activate sprint
            if (Inputs().y > 0)
            {
                pManager.playerSprint = true;
                pManager.playerSpeed = defaultSpeed * 1.5f;
            }
        }

    }

    private void CancelSprint()
    {
        hittingSprintButton = false;
        pManager.playerSprint = false;
        pManager.playerSpeed = defaultSpeed;
    }
    private Vector2 Inputs()
    {
        return pInput.actions["Movement"].ReadValue<Vector2>();

    }
    #endregion



}