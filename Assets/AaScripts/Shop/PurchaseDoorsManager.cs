using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PurchaseDoorsManager : NetworkBehaviour
{
     BoxCollider boxCollider;
     Animator anim;


    private void Awake()
    {
        
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }
    [ClientRpc]
    public void OpenDoorClientRpc()
    {
        Invoke(nameof(turnColliderOff), 0.6f);
        if (!IsServer) return;
        OpenDoorServerRpc();
    }

    [ServerRpc]
    private void OpenDoorServerRpc()
    {
        anim.SetTrigger("Open");

    }


    private void turnColliderOff()
    {
        boxCollider.enabled = false;
    }
}
