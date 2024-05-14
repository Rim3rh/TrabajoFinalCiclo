using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    #region Vars
    //Player references
    [Header("Player")]
    [SerializeField] PlayerInteract pInteract;
    AmmoManager ammoManager;
    PlayerManager pManager;
    //>>>>>>>>GESTION DE ARMAS<<<<<<<//
    //Gun animator
    Animator anim;
    //reference to gunscriptable object(data container for weapon)
    [SerializeField] GunScriptableObject gunScriptableObject;
    //private variables created so we dont modify scriptableobject
    private float shootingCD;
    private int currentMagazineAmmo;
    private int currentAmmo;
    //made public so zombie knows how much damage to recive
    [HideInInspector] public float currentWeaponDamage;
    #endregion
    #region SelfRunningMethods
    private void Awake()
    {
        //get animatior since its on the weapon
        anim = GetComponent<Animator>();
        //get references from [serializedfield] pInteract
        ammoManager = pInteract.GetComponent<AmmoManager>();
        pManager = pInteract.GetComponent<PlayerManager>();
    }
    private void Start()
    {
        //assing private variables values
        currentWeaponDamage = gunScriptableObject.damage;
        currentAmmo = gunScriptableObject.totalAmmo;
        currentMagazineAmmo = gunScriptableObject.maxMagazineAmmo;
        //create ammo pool for current weapon
        ammoManager.CreateAmmoPool(gunScriptableObject.maxMagazineAmmo, gunScriptableObject.weaponID);
        ammoManager.CreateAmmoCanvas(currentMagazineAmmo, gunScriptableObject.weaponID, currentAmmo);
    }
    private void Update()
    {
        //timer used for shootingspeed
        shootingCD -= Time.deltaTime;
    }
    private void OnEnable()
    {
        //subscribe to soot and reload events for this weapon
        pInteract.onShoot += Shoot;
        pInteract.onReload += Reload;
        //create the ammo canvas
        ammoManager.CreateAmmoCanvas(currentMagazineAmmo, gunScriptableObject.weaponID, currentAmmo);
    }
    private void OnDisable()
    {
        //Unsubscribe to soot and reload events for this weapon
        pInteract.onShoot -= Shoot;
        pInteract.onReload -= Reload;
        //disable ammo canvas
        ammoManager.RemoveAllAmmoFromCanvas();
    }
    #endregion
    #region Private Methods
    protected virtual void Shoot()
    {
        //if ur realoading cancel action
        if (pManager.isReloading) return;
        //if timer allows you to shoot and you have ammo
        if (shootingCD <= 0 && currentMagazineAmmo > 0)
        {
            //assing cd back to value specified by So(scriptable object)
            shootingCD = 1 / gunScriptableObject.shootsPS;
            //substract one ammo, we pass weapon id so it knows what weapon we want it to remove it from
            currentMagazineAmmo--;
            ammoManager.RemoveOneBulletFromAmmo(gunScriptableObject.weaponID);
            //set trigger for animator
            anim.SetTrigger("Shoot");
            //send the shooting raycast
            pInteract.ShootRaycast();
            //Audio
            //AudioManager.instance.AkSfxShoot();
        }
    }
    protected virtual void Reload()
    {
        //if ur realoading or have max ammo return
        if (pManager.isReloading) return;
        if (currentMagazineAmmo == gunScriptableObject.maxMagazineAmmo) return;
        //trigger the animation
        anim.SetTrigger("Reload");
        //remove used magazine ammo from current ammo
        currentAmmo -= gunScriptableObject.maxMagazineAmmo - currentMagazineAmmo;
        //max the magazineAmmo
        currentMagazineAmmo = gunScriptableObject.maxMagazineAmmo;
        //tell hud to reload ammo
        ammoManager.ReloadAmmo(gunScriptableObject.maxMagazineAmmo, gunScriptableObject.weaponID, currentAmmo);
        //Audio
        //AudioManager.instance.ReloadSfx();
    }
    //references for animator evets
    private void RealoadingToTrue()
    {
        pManager.isReloading = true;
    }
    private void RealoadingToFalse()
    {
        pManager.isReloading = false;
    }
    #endregion
    #region public methods
    //made public for WallWeaponBuy
    public void MaxAmmo()
    {
        //set ammo to max, without necesarily neading to reaload(if you have magazine full)
        currentAmmo = gunScriptableObject.totalAmmo;
        Reload();
        //update text ammo, sinse if u have full magazine, reload method will not do it
        ammoManager.UpdateTextAmmoHud(currentAmmo);
    }
    public int GetGunWeaponId()
    {
        //se we can know the weaponIf from So, using the reference to weapon
        return gunScriptableObject.weaponID;
    }
    #endregion
}
