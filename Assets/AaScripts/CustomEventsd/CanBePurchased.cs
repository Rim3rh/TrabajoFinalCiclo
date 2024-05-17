using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
//we want all obj with this class to have a boxcollider as its needed for its behaveur
[RequireComponent(typeof(BoxCollider))]
public class CanBePurchased : NetworkBehaviour
{
    public UnityEvent onEnter;
    [SerializeField] int moneyNeeded;
    private GameObject player;
    bool canBoPurchased = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            PlayerInteract pInteract = player.GetComponent<PlayerInteract>();
            UiManager uiManager = player.GetComponent<UiManager>();
            PlayerManager playerManager = player.GetComponent<PlayerManager>();
            pInteract.onInteract += InvokeEnevt;
            uiManager.ShowPrice(moneyNeeded);
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
        if (!canBoPurchased) return;
        if(player.GetComponent<PlayerManager>().PlayerPoints >= moneyNeeded)
        {
            //AudioManager.instance.BuyFromShop();
            player.GetComponent<PlayerManager>().PlayerPoints -= moneyNeeded;
            OnEnterServerRpc();
            player.GetComponent<UiManager>().HidePrice();
            canBoPurchased = false;
        }
    }

    [ServerRpc(RequireOwnership =false)]
    private void OnEnterServerRpc()
    {
        onEnter?.Invoke();
    }
}
