using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WallBuyManager : MonoBehaviour
{
    //Private reference setted when player enters the weaponbuy area
    private GameObject player;
    //Moneyn needed to by weapon or ammo
    int moneyNeeded;
    //money setted from inspector for weapon/ammo
    [SerializeField] int weaponCost, weaponAmmoCost;
    [SerializeField] int weaponId;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //get the player references
            player = other.gameObject;
            PlayerInteract pInteract = player.GetComponent<PlayerInteract>();
            UiManager uiManager = player.GetComponent<UiManager>();
            PlayerManager playerManager = player.GetComponent<PlayerManager>();
            //Decide price depending on if player has weapon or not
            DecideWhatPlayerIsBuying(player);
            //Show price
            uiManager.ShowPrice(moneyNeeded);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //get the player references
            player = other.gameObject;
            PlayerInteract pInteract = player.GetComponent<PlayerInteract>();
            UiManager uiManager = player.GetComponent<UiManager>();
            PlayerManager playerManager = player.GetComponent<PlayerManager>();
            //We unsubscribe from both methods just in case
            pInteract.onInteract -= BuyWeapon;
            pInteract.onInteract -= BuyAmmo;
            //Hoide price
            uiManager.HidePrice();
        }
    }

    private void DecideWhatPlayerIsBuying(GameObject player)
    {
        //Player References
        WeaponManager wManager = player.GetComponent<WeaponManager>();
        PlayerInteract pInteract = player.GetComponent<PlayerInteract>();
        //we check if there is a second weapon, if there is not you can not have any other weapon apart from pistol
        if(wManager.secondaryWeapon == null)
        {
            moneyNeeded = weaponCost;
            pInteract.onInteract += BuyWeapon;
            return;
        }
        //check if player has weapon or not
        if (wManager.secondaryWeapon.GetGunWeaponId()  == weaponId || wManager.currentWeapon.GetGunWeaponId() == weaponId)
        {
            moneyNeeded = weaponAmmoCost;
            pInteract.onInteract += BuyAmmo;

        }
        else
        {
            moneyNeeded = weaponCost;
            pInteract.onInteract += BuyWeapon;
        }

    }
    private void BuyWeapon()
    {
        if (player.GetComponent<PlayerManager>().PlayerPoints >= moneyNeeded)
        {
            //AudioManager.instance.BuyFromShop();
            //remove money from player
            player.GetComponent<PlayerManager>().PlayerPoints -= moneyNeeded;
            //hide the price since u already bought it
            player.GetComponent<UiManager>().HidePrice();
            //set the new waepon
            player.GetComponent<WeaponManager>().SetNewWeapon(weaponId);
        }
    }

    private void BuyAmmo()
    {
        if (player.GetComponent<PlayerManager>().PlayerPoints >= moneyNeeded)
        {
            //AudioManager.instance.BuyFromShop();
            //take money
            player.GetComponent<PlayerManager>().PlayerPoints -= moneyNeeded;
            //hide price
            player.GetComponent<UiManager>().HidePrice();
            //if player is holding the weapon max its amo, if he is not, swap weapon and max its ammo
            if(player.GetComponent<WeaponManager>().currentWeapon.GetGunWeaponId() == weaponId)
            {
                player.GetComponent<WeaponManager>().currentWeapon.MaxAmmo();
            }
            else
            {
                player.GetComponent<WeaponManager>().SwitchToNextWeapon();
                player.GetComponent<WeaponManager>().currentWeapon.MaxAmmo();
            }
        }
    }

}
