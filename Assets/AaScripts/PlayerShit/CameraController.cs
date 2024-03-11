using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraController : MonoBehaviour
{

    private PlayerManager pManager;

    [SerializeField] Transform cameraTransform;
    private float desiredX, xRotation;

    private void Awake()
    {
        pManager = GetComponent<PlayerManager>();

    }
    private void Update()
    {
        cameraTransform.transform.position = this.transform.position;
        Look();
    }


    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * pManager.sensitivity * Time.fixedDeltaTime * pManager.sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * pManager.sensitivity * Time.fixedDeltaTime * pManager.sensMultiplier;

        //Find current look rotation


        Vector3 rot = cameraTransform.transform.localRotation.eulerAngles;
        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        desiredX = rot.y + mouseX;

        cameraTransform.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
    }
}
