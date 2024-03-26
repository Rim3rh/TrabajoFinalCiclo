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
    #region VARS
    //class references
    private PlayerManager pManager;
    //CameraTrasform so I can change its rotation
    [SerializeField] Transform cameraTrasnform;
    //need the camera so we can change the priority
    [SerializeField] CinemachineVirtualCamera cam;
    //CameraROtation based on the mouse movement
    private float xRotation;
    #endregion
    #region SelfRunningMethods
    private void Awake()
    {
        //getting component
        pManager = GetComponent<PlayerManager>();
    }
    private void Start()
    {
        //Dont want notowners to change cam priority
        if (!IsOwner) return;
        cam.Priority = 20;
    }
    private void Update()
    {
        //nopt owners should not rotate the camera
        if (!IsOwner) return;
        Look();
    }
    #endregion
    #region Private Methods
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * pManager.sensitivity * Time.fixedDeltaTime * pManager.sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * pManager.sensitivity * Time.fixedDeltaTime * pManager.sensMultiplier;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -35f, 45f);

        cameraTrasnform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
    #endregion
}
