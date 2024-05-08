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
        anim.SetTrigger("Open");
        Invoke(nameof(turnColliderOff), 0.6f);
    }



    private void turnColliderOff()
    {
        boxCollider.enabled = false;
    }
}
