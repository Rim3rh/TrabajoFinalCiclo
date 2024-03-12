using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraController : MonoBehaviour
{
    //References
    private PlayerManager pManager;

    //Serializable
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform weaponTransform;

    [SerializeField] Transform cameraPos;
    [SerializeField] Transform weaponPos;


    //CameraROtationShit
    private float desiredX, xRotation;

    private void Awake()
    {
        pManager = GetComponent<PlayerManager>();

    }
    private void Update()
    {
        cameraTransform.transform.position = cameraPos.transform.position;
        weaponTransform.transform.position = weaponPos.transform.position;

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

        cameraTransform.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        weaponTransform.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);


        this.transform.localRotation = Quaternion.Euler(transform.rotation.x, desiredX, transform.rotation.z);

    }
}
