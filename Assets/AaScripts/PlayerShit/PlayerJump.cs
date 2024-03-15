using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerJump : NetworkBehaviour
{
    PlayerManager pManager;
    PlayerInput pInput;
    Rigidbody rb;
    PlayerAnimationController animController;



    private void Awake()
    {
        pManager = GetComponent<PlayerManager>();
        pInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        animController = GetComponent<PlayerAnimationController>();
    }
    private void Start()
    {
        pInput.actions["Jump"].started += PlayerJump_started;
    }

    private void PlayerJump_started(InputAction.CallbackContext obj)
    {
        if (!IsOwner) return;

        if (!pManager.isPlayerGrounded) return;
        rb.AddForce(rb.velocity.x, pManager.playerJumpForce, rb.velocity.z, ForceMode.Impulse);
        //animController.Jump();
        //AudioManager.instance.PlayerJumpSfx();
    }
}
