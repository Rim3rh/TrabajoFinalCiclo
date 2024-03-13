using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;

public class WeaponManager : MonoBehaviour
{

    //PlayerInput
    PlayerInput pInput;

    //WEapon logic
    public GameObject[] abeliableWeapons;
    [SerializeField] GameObject currentWeapon, secondaryWeapon;
    [SerializeField] List<GameObject> weaponSlots = new List<GameObject>();




    //animationLogic
    [SerializeField] Animator animator;
    [HideInInspector] public bool changingWeapon;

    private void Awake()
    {
        pInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {

        TurnAllWeaponsOff();


        //Start with pistol

        abeliableWeapons[0].SetActive(true);
        currentWeapon = abeliableWeapons[0];
        weaponSlots.Add(currentWeapon);

        //GET RIFLE
        weaponSlots.Add(abeliableWeapons[1]);
        secondaryWeapon = abeliableWeapons[1];
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
        AudioManager.instance.PlayerSwitchWeapon();
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

    private void TurnAllWeaponsOff()
    {
        foreach (var w in abeliableWeapons)
        {
            w.SetActive(false);
        }
    }

    public void SetNewWeapon(GameObject weapon)
    {
        currentWeapon.SetActive(false);
        currentWeapon = weapon;
        weaponSlots.Clear();
        weaponSlots.Add(currentWeapon);
        weaponSlots.Add(secondaryWeapon);
        UpdateWeapons();
    }
}
