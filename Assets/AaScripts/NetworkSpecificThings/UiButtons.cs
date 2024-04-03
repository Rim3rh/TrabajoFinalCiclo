using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UiButtons : MonoBehaviour
{
    [SerializeField] Button clientButton, hostButton;


    private void Awake()
    {

        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            NetworkManager.Singleton.SceneManager.LoadScene("LobbyScene", UnityEngine.SceneManagement.LoadSceneMode.Single);


        });
        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();

        });

    }






    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
