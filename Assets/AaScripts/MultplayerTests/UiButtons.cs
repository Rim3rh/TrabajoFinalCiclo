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
        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            Hide();
        });

        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            Hide();

        });
    }






    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
