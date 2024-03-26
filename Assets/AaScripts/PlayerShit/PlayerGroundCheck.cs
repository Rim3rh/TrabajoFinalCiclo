using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class PlayerGroundCheck : NetworkBehaviour
{
    #region VARS
    //Class references
    PlayerManager pManger;

    //position from witch the box will be created
    [SerializeField] Transform groundCheckPos;
    //Ground Mask
    [SerializeField] LayerMask groundMask;
    //Size the box will have when created from GroundCheckPos
    [SerializeField] Vector3 cubeSize;

    #endregion
    #region SelfRunningMethods
    private void Awake()
    {
        pManger = GetComponent<PlayerManager>();
    }
    void Update()
    {
        //dont want other users to check for this raycast(dont know if it would be an issue, but even then woulent make sense optimation wise.)
        if (!IsOwner) return;
        //if the overlapBox(cube created) hits something in the groundmask, leght will be 1 else 0
        Collider[] colliders = Physics.OverlapBox(groundCheckPos.transform.position, cubeSize * 2, Quaternion.identity, groundMask);
        //assing isPlayerGrounded to the logic before
        pManger.isPlayerGrounded = colliders.Length > 0;
        //if your not grounded and the overlapBox hits something, means you landed so land Logic.
        Land(colliders);



    }
    #endregion
    #region Private Methods
    private void Land(Collider[] colliders)
    {
        if (!pManger.isPlayerGrounded && colliders.Length > 0)
        {
            //Audio Disabled For now
            //AudioManager.instance.PlayerLandSfx();
        }
    }
    /*
     * CHeck the actual size of the overLapBox
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPos.transform.position, cubeSize);
    }
    */
    #endregion
}
