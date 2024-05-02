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
    private void Update()
    {
        //even when not suscribed,, no reason to check if shoting if im not owner.
        if(!IsOwner) return;
        //if shooting is true invokke the event
        if (shooting) onShoot?.Invoke();
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
    /*
    public void PlaceHole()
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
                hit.collider.GetComponent<IShooteable>().FindPlayer
                (this.gameObject);

                hit.collider.GetComponent<IShooteable>().TakeDamge
                (weaponManager.currentWeapon.currentWeaponDamage);
            }


            //Wall Holes object pool
            GameObject go = GetHole();
            go.SetActive(true);
            go.transform.SetPositionAndRotation(hit.point, Quaternion.LookRotation(hit.normal));
            go.transform.parent = hit.collider.transform;

        }
    }
    

    private GameObject GetHole()
    {
        //reset lastGo so it cant give the same hole as last intineration
        GameObject lastGo = null;
        //loop to check if there are any abeliable holes on the list(if they are inactive they are abeliable)
        foreach (GameObject go in BulletHolesPoolManager.holeList)
        {
            if (go.activeSelf == false)
            {
                //if one aveliable is found, retunr it as the hole.
                lastGo = null;
                return go;
            }
            lastGo = go;
        }


        if (lastGo != null)
        {
            //if no Go is aveliable, get the frist one on the list(first one placed)
            GameObject go = BulletHolesPoolManager.holeList[1];
            //remove it from the list and add it so its now on the last place of the list.
            BulletHolesPoolManager.holeList.Remove(go);
            BulletHolesPoolManager.holeList.Add(go);
            //return it
            return go;
        }
        //Code will never reach this poiunt but unity gets madf if you dont check for all scenrarios.
        return null;
    }
    */
    #endregion
}
