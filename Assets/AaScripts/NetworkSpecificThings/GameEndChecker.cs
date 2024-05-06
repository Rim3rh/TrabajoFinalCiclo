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

    [ClientRpc]

    private void LoadMainMenuClientRpc()
    {
        SceneManager.LoadScene(0);
    }
}
