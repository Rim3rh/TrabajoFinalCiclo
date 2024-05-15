using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerJump : NetworkBehaviour
{
    #region VARS
    //Class References
    PlayerManager pManager;
    PlayerAnimationController animController;
    //Component References
    PlayerInput pInput;
    Rigidbody rb;
    #endregion
    #region SelfRunningMethods
    private void Awake()
    {
        //Component Assignation
        pManager = GetComponent<PlayerManager>();
        pInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        animController = GetComponent<PlayerAnimationController>();
    }
    private void Start()
    {
        //CHeck for owner so only owner subscrives to event
        if (!IsOwner) return;
        //Subscribe to the playerInput Event
        pInput.actions["Jump"].started += PlayerJump_started;
    }

    private void PlayerJump_started(InputAction.CallbackContext obj)
    {
        //If notGrounded return
        if (!pManager.isPlayerGrounded) return;
        //apply the force
        rb.AddForce(rb.velocity.x, pManager.playerJumpForce, rb.velocity.z, ForceMode.Impulse);
        //Call the Jump Animation
        animController.Jump();
        //AudioDisabledForNow
        AudioManager.instance.PlayerJumpSfx(this.transform.position);
    }
    #endregion
}
