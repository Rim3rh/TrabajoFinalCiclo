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
    [SerializeField] Transform camTrasnform;
    //[SerializeField] Transform weaponTransform;

    [SerializeField] CinemachineVirtualCamera cam;
    
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
        cam.Priority = 20;
        //weaponTransform.transform.position = weaponPos.transform.position;

        Look();
    }


    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * pManager.sensitivity * Time.fixedDeltaTime * pManager.sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * pManager.sensitivity * Time.fixedDeltaTime * pManager.sensMultiplier;


        Vector3 rot = camTrasnform.transform.localRotation.eulerAngles;
        

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -70f, 70f);

        desiredX = rot.y + mouseX;


        camTrasnform.transform.rotation = Quaternion.Euler(xRotation, desiredX, camTrasnform.transform.rotation.z);

    }
}
