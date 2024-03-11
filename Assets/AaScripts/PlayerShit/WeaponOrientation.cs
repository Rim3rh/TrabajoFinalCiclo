using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOrientation : MonoBehaviour
{
    [SerializeField] Transform zRotation, xRotation;

    private void Update()
    {
        transform.rotation = Quaternion.Euler(xRotation.localRotation.x, transform.rotation.y, zRotation.localRotation.z);


    }



}
