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

    private float shootingCD;
    private int currentAmmo;

    



    private void Awake()
    {
        anim = GetComponent<Animator>();
        ammoManager = pInteract.GetComponent<AmmoManager>();
    }

    private void Start()
    {

        MyEnum weapon = myEnum;
        weaponID = (int)weapon;
        currentAmmo = ammo;
        ammoManager.CreateAmmoPool(ammo, weaponID);
        ammoManager.CreateAmmoCanvas(currentAmmo, weaponID);

    }
    private void OnEnable()
    {

        pInteract.onShoot += Shoot;
        pInteract.onReload += Reload;
        ammoManager.CreateAmmoCanvas(currentAmmo, weaponID);

    }

    private void OnDisable()
    {
        pInteract.onShoot -= Shoot;
        pInteract.onReload -= Reload;
        ammoManager.RemoveAllAmmoFromCanvas(ammo);


    }
    protected virtual void Shoot()
    {
        if (shootingCD <= 0 && currentAmmo > 0)
        {
            shootingCD = 1 / shootsPS;
            currentAmmo--;

            //Shoot
            ammoManager.RemoveOneBulletFromAmmo(weaponID);

            anim.SetTrigger("Shoot");
            //AudioManager.instance.AkSfxShoot();
            pInteract.PlaceHole();
        }
    }

    protected virtual void Reload()
    {
        anim.SetTrigger("Reload");
        //AudioManager.instance.ReloadSfx();
        currentAmmo = ammo;
        ammoManager.CreateAmmoCanvas(currentAmmo, weaponID);


    }
    private void Update()
    {
        shootingCD -= Time.deltaTime;
    }
}
