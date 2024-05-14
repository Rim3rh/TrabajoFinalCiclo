using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsFollowGuns : MonoBehaviour
{
    #region Vars
    //reference to players waponmanager
    [SerializeField] WeaponManager manager;
    //reference to hands
    [SerializeField] GameObject leftHand, rightHand;
    //hand positions that will get used for tacking 
    [Header("-----PISTOL-----")]
    [SerializeField] GameObject pistolLeftHandPos, pistolRightHandPos;
    [Header("-----AK-----")]
    [SerializeField] GameObject akLeftHandPos, akRightHandPos;
    [Header("-----Sniper-----")]
    [SerializeField] GameObject sniperLeftHandPos, sniperRightHandPos;
    [Header("-----Sniper-----")]
    [SerializeField] GameObject m4LeftHandPos, m4RightHandPos;
    #endregion
    private void Update()
    {
        //create variables, and asing them values sinse code is not certain the ifs will work.
        Vector3 rightHandPos = Vector3.zero;
        Vector3 leftHandPos = Vector3.zero;
        Quaternion rightHandPosRot = Quaternion.identity;
        Quaternion leftHandPosRot = Quaternion.identity;

        //depending on the weapon, set the hands position and rotation
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
            leftHandPos = m4LeftHandPos.transform.position;
            leftHandPosRot = m4LeftHandPos.transform.rotation;

            rightHandPos = m4RightHandPos.transform.position;
            rightHandPosRot = m4RightHandPos.transform.rotation;
        }
        //Aply the position and rotation to both hands
        leftHand.transform.SetPositionAndRotation(leftHandPos, leftHandPosRot);
        rightHand.transform.SetPositionAndRotation(rightHandPos, rightHandPosRot);
    }
}
