using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class CanBeShoot : NetworkBehaviour
{
    //event called when shoot
    public UnityEvent customEvent;
    public void ReciveShoot()
    {
        //will tell server, to tell al clients to invoke the event
        CallReciveShootServerRpc();
    }
    [ServerRpc(RequireOwnership =false)]
    private void CallReciveShootServerRpc()
    {
        CallReciveShootClientRpc();
    }
    [ClientRpc]
    private void CallReciveShootClientRpc()
    { 
        customEvent?.Invoke();
    }
}
