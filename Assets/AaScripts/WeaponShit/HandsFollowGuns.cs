using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsFollowGuns : MonoBehaviour
{
    [SerializeField] WeaponManager manager;
    [SerializeField] GameObject leftHand, rightHand;
    [SerializeField] GameObject leftHand2, rightHand2;


    [Header("-----PISTOL-----")]
    [SerializeField] GameObject pistolLeftHandPos, pistolRightHandPos;

    [Header("-----AK-----")]
    [SerializeField] GameObject akLeftHandPos, akRightHandPos;
    [Header("-----Sniper-----")]
    [SerializeField] GameObject sniperLeftHandPos, sniperRightHandPos;
    private void Update()
    {
        Vector3 rightHandPos = Vector3.zero;
        Vector3 leftHandPos = Vector3.zero;
        Quaternion rightHandPosRot = Quaternion.identity;
        Quaternion leftHandPosRot = Quaternion.identity;

        if (manager.currentWeapon == manager.abeliableWeapons[0])
        {
            leftHandPos = pistolLeftHandPos.transform.position;
            leftHandPosRot = pistolLeftHandPos.transform.rotation;

            rightHandPos = pistolRightHandPos.transform.position;
            rightHandPosRot = pistolRightHandPos.transform.rotation;
        }
        if (manager.currentWeapon == manager.abeliableWeapons[1])
        {
            leftHandPos = akLeftHandPos.transform.position;
            leftHandPosRot = akLeftHandPos.transform.rotation;

            rightHandPos = akRightHandPos.transform.position;
            rightHandPosRot = akRightHandPos.transform.rotation;
        }
        if (manager.currentWeapon == manager.abeliableWeapons[2])
        {
            leftHandPos = sniperLeftHandPos.transform.position;
            leftHandPosRot = sniperLeftHandPos.transform.rotation;

            rightHandPos = sniperRightHandPos.transform.position;
            rightHandPosRot = sniperRightHandPos.transform.rotation;
        }

        leftHand.transform.SetPositionAndRotation(leftHandPos, leftHandPosRot);
        rightHand.transform.SetPositionAndRotation(rightHandPos, rightHandPosRot);
        leftHand2.transform.SetPositionAndRotation(leftHandPos, leftHandPosRot);
        rightHand2.transform.SetPositionAndRotation(rightHandPos, rightHandPosRot);

    }
    
}
