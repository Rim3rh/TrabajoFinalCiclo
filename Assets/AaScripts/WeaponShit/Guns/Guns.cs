using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerInteract;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class Guns : MonoBehaviour
{
    //ScriptReferences
    [Header("Player")]

    [SerializeField] PlayerInteract pInteract;


    //>>>>>>>>GESTION DE ARMAS<<<<<<<//
    private enum MyEnum
    {
       
        Pistol = 1,
        Ak = 2,
        Sniper = 3
    }
    [SerializeField] MyEnum myEnum;
    int weaponID;


    [Header("WEAPONSHIT")]

    [SerializeField] int ammo;
    [SerializeField] float shootsPS;
    [SerializeField] float damage;
    Animator animator;

    private float shootingCD;
    private int currentAmmo;

    



    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {

        MyEnum weapon = myEnum;
        weaponID = (int)weapon;
        currentAmmo = ammo;
        AmmoManager.instance.CreateAmmoPool(ammo, weaponID);
        AmmoManager.instance.CreateAmmoCanvas(currentAmmo, weaponID);

    }
    private void OnEnable()
    {

        pInteract.onShoot += Shoot;
        pInteract.onReload += Reload;
        AmmoManager.instance.CreateAmmoCanvas(currentAmmo, weaponID);

    }

    private void OnDisable()
    {
        pInteract.onShoot -= Shoot;
        pInteract.onReload -= Reload;
        AmmoManager.instance.RemoveAllAmmoFromCanvas(ammo);


    }
    protected virtual void Shoot()
    {
        if (shootingCD <= 0 && currentAmmo > 0)
        {
            shootingCD = 1 / shootsPS;
            currentAmmo--;

            //Shoot
            AmmoManager.instance.RemoveOneBulletFromAmmo(weaponID);

            animator.SetTrigger("Shoot");
            //AudioManager.instance.AkSfxShoot();
            pInteract.PlaceHole();
        }
    }

    protected virtual void Reload()
    {
        animator.SetTrigger("Reload");
        //AudioManager.instance.ReloadSfx();
        currentAmmo = ammo;
        AmmoManager.instance.CreateAmmoCanvas(currentAmmo, weaponID);


    }
    private void Update()
    {
        shootingCD -= Time.deltaTime;
    }
}
