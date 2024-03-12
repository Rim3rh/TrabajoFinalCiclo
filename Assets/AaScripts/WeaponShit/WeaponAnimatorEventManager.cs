using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponAnimatorEventManager : MonoBehaviour
{
    [SerializeField] WeaponManager wManager;






    private void ChanginWeaponToTrue()
    {
        wManager.changingWeapon = true;
    }
    private void ChanginWeaponToFalse()
    {
        wManager.changingWeapon = false;
    }


    private void ChangeWeapon()
    {
        wManager.SwitchToNextWeapon();
    }
}
