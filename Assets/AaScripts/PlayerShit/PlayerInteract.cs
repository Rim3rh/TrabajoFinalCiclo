using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;

public class PlayerInteract : NetworkBehaviour
{
    #region VARS
    //INTERACT event
    public delegate void OnInteract();
    public OnInteract onInteract;
    //SHOOT event
    public delegate void OnShoot();
    public OnInteract onShoot;
    //shootingState
    private bool shooting;
    //Reload event
    public delegate void OnReload();
    public OnInteract onReload;
    //Change weapon event
    public delegate void OnWeaponChanged();
    public OnInteract onWeaponChanged;

    //Camera transfor and layers for shooting objcs
    [SerializeField] Transform cam;
    [SerializeField] LayerMask shooteableLayers;

    //Disabled For now
    //[SerializeField] BeatManager beatManager;

    //Component References
    PlayerInput pInput;
    WeaponManager weaponManager;

    #endregion
    #region SelfRunningMethods
    private void Awake()
    {
        //Geting Components
        pInput = GetComponent<PlayerInput>();
        weaponManager = GetComponent<WeaponManager>();
    }
    private void Start()
    {
        //Only want to subscrive to methos if im owner of the class
        if (!IsOwner) return;
        //Subscribing to the diferent Methods
        pInput.actions["Interact"].started += PlayerInteract_started;
        pInput.actions["Shoot"].started += Shoot_Started;
        pInput.actions["Shoot"].canceled += Shoot_Canceled;
        pInput.actions["Reload"].started += Reload_Started;
    }

    private float Inputs()
    {
        return pInput.actions["WeaponSwitch"].ReadValue<float>();
    }
    private void Update()
    {
        //even when not suscribed,, no reason to check if shoting if im not owner.
        if(!IsOwner) return;
        //if shooting is true invokke the event
        if (shooting) onShoot?.Invoke();
        //Invoke the onweaponChangedEvent
        if(Mathf.Abs(Inputs()) > 0) onWeaponChanged?.Invoke();
    }
    #endregion 
    #region PrivateMethods
    private void Reload_Started(InputAction.CallbackContext obj)
    {
        //invoke the event when reload button is hit
        onReload?.Invoke();
    }

    private void PlayerInteract_started(InputAction.CallbackContext obj)
    {
        //invoke the event when interac button is hit
        onInteract?.Invoke();
    }

    private void Shoot_Started(InputAction.CallbackContext obj)
    {
        //set SHooting to true when shoot button is hit
        shooting = true;
    }
    private void Shoot_Canceled(InputAction.CallbackContext obj)
    {
        //To false when its released
        shooting = false;
    }
    
    public void ShootRaycast()
    {
        //check if owner in case a not owner calls this method(might be able to be removed(not sure if it can so im leaving it xd))
        if (!IsOwner) return;
        //raycast to check if hit something
        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, Mathf.Infinity, shooteableLayers))
        {
            //this is a class with a Unity envet, so any obj with it will be able to add the logic that it wants to happen when beeing hit
            //so here, we just need to call this method that will call the event.
            if(hit.collider.GetComponent<CanBeShoot>() != null) hit.collider.GetComponent<CanBeShoot>().ReciveShoot();
            if (hit.collider.GetComponent<IShooteable>() != null)
            {
                hit.collider.GetComponent<IShooteable>().SetPlayer
                (this.gameObject);

                hit.collider.GetComponent<IShooteable>().TakeDamge
                (weaponManager.currentWeapon.currentWeaponDamage);
            }
        }
    }
    #endregion
}
