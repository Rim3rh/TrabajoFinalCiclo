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
    [SerializeField] Transform cameraTrasnform;

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
        if (!IsOwner) return;

        float mouseX = Input.GetAxis("Mouse X") * pManager.sensitivity * Time.fixedDeltaTime * pManager.sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * pManager.sensitivity * Time.fixedDeltaTime * pManager.sensMultiplier;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -35f, 45f);

        cameraTrasnform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

}
