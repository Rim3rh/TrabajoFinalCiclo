using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class CanBePurchased : NetworkBehaviour
{
    public UnityEvent onEnter;
    [SerializeField] int moneyNeeded;
    private GameObject player;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("COLLIDER");
            player = other.gameObject;
            PlayerInteract pInteract = player.GetComponent<PlayerInteract>();
            UiManager uiManager = player.GetComponent<UiManager>();
            PlayerManager playerManager = player.GetComponent<PlayerManager>();
            pInteract.onInteract += InvokeEnevt;
            uiManager.ShowPrice(playerManager.PlayerPoints);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            PlayerInteract pInteract = player.GetComponent<PlayerInteract>();
            UiManager uiManager = player.GetComponent<UiManager>();
            PlayerManager playerManager = player.GetComponent<PlayerManager>();
            pInteract.onInteract -= InvokeEnevt;
            uiManager.HidePrice();
        }
    }

    private void InvokeEnevt()
    {
        if(player.GetComponent<PlayerManager>().PlayerPoints >= moneyNeeded)
        {
            //AudioManager.instance.BuyFromShop();
            player.GetComponent<PlayerManager>().PlayerPoints -= moneyNeeded;
            OnEnterClientRpc();
        }
    }

    [ClientRpc]
    private void OnEnterClientRpc()
    {
        OnEnterServerRpc();
    }

    [ServerRpc(RequireOwnership =false)]
    private void OnEnterServerRpc()
    {
        onEnter?.Invoke();
    }
}
