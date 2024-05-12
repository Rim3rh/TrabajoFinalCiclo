using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PurchaseDoorsManager : NetworkBehaviour
{
    //reference to collider since we will be turning it off
     BoxCollider boxCollider;
    //Animator reference
     Animator anim;
    private void Awake()
    {
        //getting references
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }
    //mothod called by server from canBePurchased
    [ClientRpc]
    public void OpenDoorClientRpc()
    {
        //open the door, turn off the collider with delay so you let animation finish
        anim.SetTrigger("Open");
        Invoke(nameof(turnColliderOff), 0.6f);
    }
    private void turnColliderOff()
    {
        boxCollider.enabled = false;
    }
}
