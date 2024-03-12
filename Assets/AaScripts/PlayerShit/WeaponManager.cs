using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{

    [SerializeField] GameObject[] abeliableWeapons;
    [SerializeField] GameObject currentWeapon, secondaryWeapon;
    [SerializeField] List<GameObject> weaponSlots = new List<GameObject>();



    public delegate void OnShoot();

    public static OnShoot onShoot;

    //PlayerInput
    PlayerInput pInput;

    private void Awake()
    {
        pInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        pInput.actions["Shoot"].started += WeaponManager_started;

        TurnAllWeaponsOff();


        //Start with pistol

        abeliableWeapons[0].SetActive(true);
        currentWeapon = abeliableWeapons[0];
        weaponSlots.Add(currentWeapon);

        //GET RIFLE
        weaponSlots.Add(abeliableWeapons[1]);
        secondaryWeapon = abeliableWeapons[1];
    }

    private void WeaponManager_started(InputAction.CallbackContext obj)
    {
        if(onShoot != null) onShoot();
    }

    private void Update()
    {
        WeaponSwitch();
    }

    private float Inputs()
    {
        return pInput.actions["WeaponSwitch"].ReadValue<float>();
    }

    private void WeaponSwitch()
    {
        if (weaponSlots.Count < 2) return;

        if (Inputs() < 0)
        {
            SwitchToNextWeapon();
        }

        if (Inputs() > 0)
        {
            SwitchToPreviusWeapon();
        }

    }

    private void SwitchToNextWeapon()
    {
        weaponSlots.Clear();
        weaponSlots.Add(secondaryWeapon);
        weaponSlots.Add(currentWeapon);
        UpdateWeapons();
    }


    private void UpdateWeapons()
    {
        currentWeapon = weaponSlots[0];
        secondaryWeapon = weaponSlots[1];
        secondaryWeapon.SetActive(false);
        currentWeapon.SetActive(true);
    }
    private void SwitchToPreviusWeapon()
    {
        weaponSlots.Clear();
        weaponSlots.Add(secondaryWeapon);
        weaponSlots.Add(currentWeapon);
        UpdateWeapons();
        
    }

    private void TurnAllWeaponsOff()
    {
        foreach (var w in abeliableWeapons)
        {
            w.SetActive(false);
        }
    }
}
