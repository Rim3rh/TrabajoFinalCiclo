using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] PlayerInteract pInteract;
    [SerializeField] GameObject rifle;

    private GameObject player;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            pInteract.onInteract += GetRifle;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;

            pInteract.onInteract -= GetRifle;
        }
    }



    private void GetRifle()
    {
        Debug.Log("ENTRA");
        if (player.GetComponent<WeaponManager>().abeliableWeapons.Contains(rifle))
        {
            player.GetComponent<WeaponManager>().SetNewWeapon(rifle);
        }
    }
}
