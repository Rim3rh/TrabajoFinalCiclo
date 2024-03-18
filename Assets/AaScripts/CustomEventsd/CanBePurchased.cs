using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class CanBePurchased : MonoBehaviour
{
    public UnityEvent onEnter;
    [SerializeField] int moneyNeeded;
    private GameObject player;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            PlayerInteract.onInteract += InvokeEnevt;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            PlayerInteract.onInteract -= InvokeEnevt;
        }
    }

    private void InvokeEnevt()
    {
        if(player.GetComponent<PlayerManager>().PlayerPoints >= moneyNeeded)
        {
            //AudioManager.instance.BuyFromShop();
            player.GetComponent<PlayerManager>().PlayerPoints -= moneyNeeded;
            onEnter?.Invoke();

        }
    }
}
