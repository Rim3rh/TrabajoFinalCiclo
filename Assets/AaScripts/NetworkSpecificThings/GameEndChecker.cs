using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndChecker : NetworkBehaviour
{
    public int alivePlayers;
    public void KillOnePlayer()
    {
        if(!IsServer) return;
        alivePlayers--;
        if (alivePlayers >= 0)
        {
            LoadMainMenuClientRpc();
        }
    }
    public void ReviveOnePlayer()
    {
        if (!IsServer) return;
        alivePlayers++;

    }

    [ClientRpc]

    private void LoadMainMenuClientRpc()
    {
        SceneManager.LoadScene(0);
    }


    private int ammountOfCompletedWells;
    [SerializeField] GameObject wallToHide;
    public void OneWellCompleted()
    {
        ammountOfCompletedWells++;
        if (ammountOfCompletedWells == 3)
        {
            //OpenFinalDoor, we do it with rpc since we want all clients to do it
            DesableLastWallClientRpc();
        }
    }
    [ClientRpc]
    private void DesableLastWallClientRpc()
    {
        wallToHide.SetActive(false);
    }
}
