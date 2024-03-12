using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{

    public delegate void OnInteract();

    public static OnInteract onInteract;

    PlayerInput pInput;

    private void Awake()
    {
        
        pInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        pInput.actions["Interact"].started += PlayerInteract_started;
    }

    private void PlayerInteract_started(InputAction.CallbackContext obj)
    {
        onInteract?.Invoke();
    }
}
