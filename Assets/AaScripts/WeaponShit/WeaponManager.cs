using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;

public class WeaponManager : NetworkBehaviour
{

    //PlayerInput
    PlayerInput pInput;

    //WEapon logic
    public Gun[] abeliableWeapons;
    public Gun currentWeapon, secondaryWeapon;
    [SerializeField] List<Gun> weaponSlots = new List<Gun>();




    //animationLogic
    [SerializeField] Animator animator;
    [HideInInspector] public bool changingWeapon;

    private void Awake()
    {
        pInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        if (!IsOwner) return;

        TurnAllWeaponsOff();


        //Start with pistol

        abeliableWeapons[0].gameObject.SetActive(true);
        currentWeapon = abeliableWeapons[0];
        weaponSlots.Add(currentWeapon);

    }





    private void Update()
    {
        if (!IsOwner) return;




        WeaponSwitch();
    }

    private float Inputs()
    {
        return pInput.actions["WeaponSwitch"].ReadValue<float>();
    }

    private void WeaponSwitch()
    {
        if (weaponSlots.Count < 2 || changingWeapon) return;

        if (Inputs() < 0)
        {
            animator.SetTrigger("ChangeWeapon");
        }

        if (Inputs() > 0)
        {
            animator.SetTrigger("ChangeWeapon");
        }

    }

    public void SwitchToNextWeapon()
    {
        //AudioManager.instance.PlayerSwitchWeapon();
        weaponSlots.Clear();
        weaponSlots.Add(secondaryWeapon);
        weaponSlots.Add(currentWeapon);
        UpdateWeapons();
    }


    private void UpdateWeapons()
    {
        currentWeapon = weaponSlots[0];
        secondaryWeapon = weaponSlots[1];
        secondaryWeapon.gameObject.SetActive(false);
        currentWeapon.gameObject.SetActive(true);
    }

    private void TurnAllWeaponsOff()
    {
        foreach (var weapon in abeliableWeapons)
        {
            weapon.gameObject.SetActive(false);  
        }
    }

    public void SetNewWeapon(Gun weapon)
    {
        currentWeapon.gameObject.SetActive(false);
        currentWeapon = weapon;
        weaponSlots.Clear();
        weaponSlots.Add(currentWeapon);
        weaponSlots.Add(secondaryWeapon);
        UpdateWeapons();

    }






}
