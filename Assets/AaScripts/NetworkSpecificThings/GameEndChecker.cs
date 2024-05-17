using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndChecker : NetworkBehaviour
{
    #region Vars
    //modified from playerhealth, so we know how many players we have playing
    public int alivePlayers;
    //int keeping track of how many wells you have and how many you need
    private int ammountOfCompletedWells;
    //wall that is hiding endChest
    [SerializeField] GameObject wallToHide;
    #endregion
    #region Private Methods
    [ClientRpc]
    private void LoadMainMenuClientRpc()
    {
        //called on a clientrpc, since we want all clients to recive it
        SceneManager.LoadScene(0);
    }
    //method called when you complete one well
    public void OneWellCompleted()
    {
        //only called by server
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

    //Loading win scene
    [ServerRpc(RequireOwnership =false)]
    private void LoadWinSceneServerRpc()
    {
        LoadWinSceneClientRpc();
    }
    [ClientRpc]
    private void LoadWinSceneClientRpc()
    {
        SceneManager.LoadScene("WinScene");
    }
    #endregion
    #region public methods
    public void KillOnePlayer()
    {
        //logic only done on server
        if(!IsServer) return;
        alivePlayers--;
        if (alivePlayers <= 0)
        {
            //if 0 player alive, you do the end game logic
            LoadMainMenuClientRpc();
        }
    }
    public void ReviveOnePlayer()
    {
        //logic only done on server
        if (!IsServer) return;
        alivePlayers++;
    }

    public void WinGame()
    {
        LoadWinSceneServerRpc();
    }
    #endregion
}
