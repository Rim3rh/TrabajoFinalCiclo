using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;

public class WeaponManager : NetworkBehaviour
{
    #region Vars
    //Player references
    PlayerInteract pInteract;
    PlayerManager pManager;

    //Weapons the player can use
    public Gun[] abeliableWeapons;
    //references to the guns and the slots they ocupie
    public Gun currentWeapon, secondaryWeapon;
    [SerializeField] List<Gun> weaponSlots = new List<Gun>();

    //animationLogic
    [SerializeField] Animator animator;
    #endregion
    #region SelfRunningMethods
    private void Awake()
    {
        pInteract = GetComponent<PlayerInteract>();
        pManager = GetComponent<PlayerManager>();
    }
    private void Start()
    {
        //only owners modify their weapons
        if (!IsOwner) return;
        //turn all weapons off in case we have tuched player prefab for tests
        TurnAllWeaponsOff();
        //Start with pistol
        //activate pistol
        abeliableWeapons[0].gameObject.SetActive(true);
        //set currentWeapon as pistol
        currentWeapon = abeliableWeapons[0];
        //add weapon to slot
        weaponSlots.Add(currentWeapon);
        //add weaponswtich to onWeaponChanged event from playerinteract
        pInteract.onWeaponChanged += WeaponSwitch;
    }
    #endregion
    #region Private Methods
    private void UpdateWeapons()
    {
        //assing currentweapon and secundaryweapon dependiong on weapon slots
        currentWeapon = weaponSlots[0];
        secondaryWeapon = weaponSlots[1];
        //set secundary inactive and current active
        secondaryWeapon.gameObject.SetActive(false);
        currentWeapon.gameObject.SetActive(true);
    }
    private void TurnAllWeaponsOff()
    {
        //loop to turn all abeliableWapons off
        foreach (Gun weapon in abeliableWeapons)
        {
            weapon.gameObject.SetActive(false);
        }
    }
    private void WeaponSwitch()
    {
        //if you dont have 2 weapons retur
        if (weaponSlots.Count < 2 || pManager.isChangingWeapon || pManager.isReloading) return;
        //if you do, trigger the animation
        animator.SetTrigger("ChangeWeapon");
    }
    #endregion
    #region public methods
    public void SwitchToNextWeapon()
    {
        //AudioManager.instance.PlayerSwitchWeapon();
        //clear weaponslots
        weaponSlots.Clear();
        //add weapons in different order to weaponslots
        weaponSlots.Add(secondaryWeapon);
        weaponSlots.Add(currentWeapon);
        //update weapons
        UpdateWeapons();
    }

    public void SetNewWeapon(int weaponId)
    {
        if(secondaryWeapon == null)
        {
            //make secunadary weapon be the currentweapon
            secondaryWeapon = currentWeapon;
        }

        //turn current weapon off
        currentWeapon.gameObject.SetActive(false);
        //set currentweapon to desiered weapon id(-1 because weapon Id does not have 0)
        currentWeapon = abeliableWeapons[weaponId - 1];
        //clear weaponslots
        weaponSlots.Clear();
        //add currentweapon(new wapon)
        weaponSlots.Add(currentWeapon);
        //and secondary(old weapon)
        weaponSlots.Add(secondaryWeapon);
        //update weapons
        UpdateWeapons();
    }
    #endregion
}
