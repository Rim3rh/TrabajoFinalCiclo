using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConectedPlayersVisuals : NetworkBehaviour
{
    [SerializeField] GameObject secondPlayerVisuals;
    void Start()
    {
        Debug.Log("HOLA");
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
