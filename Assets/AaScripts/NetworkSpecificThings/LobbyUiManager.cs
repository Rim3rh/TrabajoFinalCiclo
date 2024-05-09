using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUiManager : NetworkBehaviour
{
    //reference to button
    [SerializeField] Button playButton;
    void Awake()
    {
        //add listener to the button
        playButton.onClick.AddListener(LoadSceneServerRpc);
    }
    //RequireOwnership set to false because we want clients to be able to start game
    [ServerRpc(RequireOwnership = false)]
    private void LoadSceneServerRpc()
    {
        //(executed only on the server) will load mainscene on both server and client
        NetworkManager.Singleton.SceneManager.LoadScene("MainScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }


}
