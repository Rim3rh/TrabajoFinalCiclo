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
       
        if(player.GetComponent<PlayerManager>().PlayerPoints >= moneyNeeded)
        {
            Debug.Log(OwnerClientId);

            //AudioManager.instance.BuyFromShop();
            player.GetComponent<PlayerManager>().PlayerPoints -= moneyNeeded;
            OnEnterServerRpc();
            player.GetComponent<UiManager>().HidePrice();
        }
    }

    [ServerRpc(RequireOwnership =false)]
    private void OnEnterServerRpc()
    {
        Debug.Log("LLEGO HASTA AQUI");
        onEnter?.Invoke();
    }
}
