using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiButtons : MonoBehaviour
{
    //class to select if you want to create a game or join one
    [SerializeField] Button clientButton, hostButton;
    private void Awake()
    {
        //add to host the following things
        hostButton.onClick.AddListener(() =>
        {
            //start host on NetworkManager
            NetworkManager.Singleton.StartHost();
            //loadscene on networkmanager(this will make client join scene when created)
            NetworkManager.Singleton.SceneManager.LoadScene("LobbyScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
        });
        //do this  for singleplayer
        if (clientButton == null) return;
        //add client on NetworkManager
        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }
    public void LoadMultiplayerMenu()
    {
        SceneManager.LoadScene("MultiPlayerMenu");
    }
}
