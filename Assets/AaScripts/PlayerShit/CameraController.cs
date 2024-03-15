using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Unity.Netcode;

public class CameraController : NetworkBehaviour
{
    //References
    private PlayerManager pManager;

    //Serializable
    [SerializeField] Transform cameraTransform;
    //[SerializeField] Transform weaponTransform;

    [SerializeField] Transform cameraPos;
   // [SerializeField] Transform weaponPos;


    //CameraROtationShit
    private float desiredX, xRotation;

    private void Awake()
    {
        pManager = GetComponent<PlayerManager>();
        //ameraTransform = GameObject.FindAnyObjectByType<CinemachineVirtualCamera>().transform;

    }
    private void Update()
    {
        if (!IsOwner) return;

        cameraTransform.transform.position = cameraPos.transform.position;
        //weaponTransform.transform.position = weaponPos.transform.position;

        Look();
    }


    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * pManager.sensitivity * Time.fixedDeltaTime * pManager.sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * pManager.sensitivity * Time.fixedDeltaTime * pManager.sensMultiplier;


        Vector3 rot = cameraTransform.transform.localRotation.eulerAngles;
        

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        desiredX = rot.y + mouseX;

        //cameraTransform.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        //weaponTransform.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);


        this.transform.localRotation = Quaternion.Euler(transform.rotation.x, desiredX, transform.rotation.z);

    }
}
