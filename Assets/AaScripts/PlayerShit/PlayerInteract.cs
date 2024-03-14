using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;

public class PlayerInteract : MonoBehaviour
{
    //INTERACT
    public delegate void OnInteract();
    public static OnInteract onInteract;
    //SHOOT
    public delegate void OnShoot();
    public OnInteract onShoot;
    private bool shooting;
 
    //Reload
    public delegate void OnReload();
    public OnInteract onReload;


    [SerializeField] Transform cam;
    [SerializeField] LayerMask shooteableLayers;
    [SerializeField] BeatManager beatManager;

    //INPUT
    PlayerInput pInput;
    private void Awake()
    {
        pInput = GetComponent<PlayerInput>();
    }



    private void Start()
    {
        pInput.actions["Interact"].started += PlayerInteract_started;
        pInput.actions["Shoot"].started += Shoot_Started;
        pInput.actions["Shoot"].canceled += Shoot_Canceled;

        pInput.actions["Reload"].started += Reload_Started;

    }


    private void Update()
    {
        if (shooting) onShoot?.Invoke();

    }
    private void Reload_Started(InputAction.CallbackContext obj)
    {
        onReload?.Invoke();

    }

    private void PlayerInteract_started(InputAction.CallbackContext obj)
    {
        onInteract?.Invoke();
    }

    private void Shoot_Started(InputAction.CallbackContext obj)
    {
        shooting = true;
    }
    private void Shoot_Canceled(InputAction.CallbackContext obj)
    {
        shooting = false;
    }


    public void PlaceHole()
    {
        if (beatManager.inBeat) beatManager.AddToCombo();
        else beatManager.CancelCombo();

        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, Mathf.Infinity, shooteableLayers))
        {
            //ESTO ES GOD
            if (hit.collider.GetComponent<CanBeShoot>() != null) hit.collider.GetComponent<CanBeShoot>().ReciveShoot();



            GameObject go = GetHole();
            go.SetActive(true);
            go.transform.SetPositionAndRotation(hit.point, Quaternion.LookRotation(hit.normal));
            go.transform.parent = hit.collider.transform;

        }
    }

    private GameObject GetHole()
    {
        GameObject lastGo = null;

        foreach (GameObject go in BulletHolesPoolManager.holeList)
        {
            if (go.activeSelf == false)
            {
                lastGo = null;
                return go;
            }
            lastGo = go;
        }


        if (lastGo != null)
        {
            GameObject go = BulletHolesPoolManager.holeList[1];
            BulletHolesPoolManager.holeList.Remove(go);
            BulletHolesPoolManager.holeList.Add(go);
            return go;
        }

        return null;
    }

}
