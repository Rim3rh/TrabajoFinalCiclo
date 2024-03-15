using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class PlayerGroundCheck : NetworkBehaviour
{

    PlayerManager pManger;
    PlayerAnimationController animController;


    [SerializeField] Transform groundCheckPos;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Vector3 cubeSize;


    private void Awake()
    {
        pManger = GetComponent<PlayerManager>();
        animController = GetComponent<PlayerAnimationController>();
    }
    void Update()
    {
        if (!IsOwner) return;

        Collider[] colliders = Physics.OverlapBox(groundCheckPos.transform.position, cubeSize * 2, Quaternion.identity, groundMask);

        if (!pManger.isPlayerGrounded && colliders.Length > 0)
        {
            //animController.Land();
            //AudioManager.instance.PlayerLandSfx();
        }

        pManger.isPlayerGrounded = colliders.Length > 0;


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPos.transform.position, cubeSize);
    }

}
