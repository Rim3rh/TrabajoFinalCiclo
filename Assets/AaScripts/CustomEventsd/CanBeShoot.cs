using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class CanBeShoot : NetworkBehaviour
{
    public UnityEvent customEvent;


    public void ReciveShoot()
    {
        Debug.Log("0");

        CallReciveShootServerRpc();
    }

    [ServerRpc(RequireOwnership =false)]
    private void CallReciveShootServerRpc()
    {
        Debug.Log("1");
        CallReciveShootClientRpc();
    }

    [ClientRpc]
    private void CallReciveShootClientRpc()
    {
        Debug.Log("2");
        customEvent?.Invoke();
    }


}
