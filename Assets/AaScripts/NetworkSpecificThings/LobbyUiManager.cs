using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUiManager : NetworkBehaviour
{
    [SerializeField] Button playButton;
    void Awake()
    {
        playButton.onClick.AddListener(LoadSceneServerRpc);
    }
    [ServerRpc(RequireOwnership = false)]

    private void LoadSceneServerRpc()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("MainScene", UnityEngine.SceneManagement.LoadSceneMode.Single);

    }


}
