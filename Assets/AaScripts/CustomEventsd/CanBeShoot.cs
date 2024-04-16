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
        CallReciveShootServerRpc();
    }

    [ServerRpc(RequireOwnership =false)]
    private void CallReciveShootServerRpc()
    {
        customEvent?.Invoke();
    }

    [ClientRpc]
    private void CallReciveShootClientRpc()
    {
       
    }


}
