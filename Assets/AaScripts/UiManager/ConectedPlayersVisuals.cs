using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConectedPlayersVisuals : NetworkBehaviour
{
    //class used on lobby to know when second playuer has joined
    [SerializeField] GameObject secondPlayerVisuals;
    void Start()
    {
        if(IsServer) {  return; }
        SetGoToActiveServerRpc();
    }



    [ServerRpc(RequireOwnership = false)]

    private void SetGoToActiveServerRpc()
    {

        SetGoToActiveClientRpc();
    }
    [ClientRpc]

    private void SetGoToActiveClientRpc()
    {
        secondPlayerVisuals.SetActive(true);

    }

}
