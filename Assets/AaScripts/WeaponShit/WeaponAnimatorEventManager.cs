using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponAnimatorEventManager : MonoBehaviour
{
    //serialized reference for weaponManager(Player)
    [SerializeField] WeaponManager wManager;
    //player manager reference
    PlayerManager pManager;

    private void Awake()
    {
        //getting reference trhought wManager
        pManager = wManager.GetComponent<PlayerManager>();
    }

    #region Animator References
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
    #endregion
}
