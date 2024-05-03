using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerInteract;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public abstract class Gun : MonoBehaviour
{
    //ScriptReferences
    [Header("Player")]
     Animator anim;

    [SerializeField] PlayerInteract pInteract;
    AmmoManager ammoManager;



    //>>>>>>>>GESTION DE ARMAS<<<<<<<//
    [SerializeField] GunScriptableObject gunScriptableObject;

    private float shootingCD;
    private bool isReloading;
    private int currentAmmo;

    public float currentWeaponDamage;



    private void Awake()
    {
        anim = GetComponent<Animator>();
        ammoManager = pInteract.GetComponent<AmmoManager>();
    }

    private void Start()
    {
        currentWeaponDamage = gunScriptableObject.damage;

        currentAmmo = gunScriptableObject.ammo;
        ammoManager.CreateAmmoPool(gunScriptableObject.ammo, gunScriptableObject.weaponID);
        ammoManager.CreateAmmoCanvas(currentAmmo, gunScriptableObject.weaponID);

    }
    private void OnEnable()
    {

        pInteract.onShoot += Shoot;
        pInteract.onReload += Reload;
        ammoManager.CreateAmmoCanvas(currentAmmo, gunScriptableObject.weaponID);

    }

    private void OnDisable()
    {
        pInteract.onShoot -= Shoot;
        pInteract.onReload -= Reload;
        ammoManager.RemoveAllAmmoFromCanvas(gunScriptableObject.ammo);


    }
    protected virtual void Shoot()
    {
        if (isReloading) return;
        if (shootingCD <= 0 && currentAmmo > 0)
        {
            shootingCD = 1 / gunScriptableObject.shootsPS;
            currentAmmo--;

            //Shoot
            ammoManager.RemoveOneBulletFromAmmo(gunScriptableObject.weaponID);

            anim.SetTrigger("Shoot");
            //AudioManager.instance.AkSfxShoot();
            pInteract.PlaceHole();
        }
    }

    protected virtual void Reload()
    {
        if (isReloading) return;
        if (currentAmmo == gunScriptableObject.ammo) return;
        anim.SetTrigger("Reload");
        //AudioManager.instance.ReloadSfx();
        currentAmmo = gunScriptableObject.ammo;
        ammoManager.ReloadAmmo(gunScriptableObject.ammo, gunScriptableObject.weaponID);


    }
    private void Update()
    {
        shootingCD -= Time.deltaTime;
    }



    private void RealoadingToTrue()
    {
        isReloading = true;
    }

    private void RealoadingToFalse()
    {
        isReloading = false;
    }
}
