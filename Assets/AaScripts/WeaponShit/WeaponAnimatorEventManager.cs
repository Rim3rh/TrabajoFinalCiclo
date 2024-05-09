using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponAnimatorEventManager : MonoBehaviour
{
    [SerializeField] WeaponManager wManager;
    PlayerManager pManager;

    private void Awake()
    {
        pManager = wManager.GetComponent<PlayerManager>();
    }




    private void ChanginWeaponToTrue()
    {
        pManager.isChangingWeapon = true;
    }
    private void ChanginWeaponToFalse()
    {
        pManager.isChangingWeapon = false;
    }


    private void ChangeWeapon()
    {
        wManager.SwitchToNextWeapon();
    }
}
