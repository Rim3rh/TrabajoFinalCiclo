using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rifle : Guns
{
    [SerializeField] PlayerInteract pInteract;

    Animator animator;
    private float shootingCD;
    private int currentAmmo;




    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        ammo = 30;
        shootsPS = 4;
        currentAmmo = ammo;
        AmmoManager.instance.CreateAmmoPool(ammo,2);
        AmmoManager.instance.CreateAmmoCanvas(currentAmmo,  2);

    }

    private void OnEnable()
    {

        pInteract.onShoot += Shoot;
        pInteract.onReload += Reload;

        AmmoManager.instance.CreateAmmoCanvas(currentAmmo,  2);

    }

    private void OnDisable()
    {
        pInteract.onShoot -= Shoot;
        pInteract.onReload -= Reload;

        AmmoManager.instance.RemoveAllAmmoFromCanvas(ammo);

    }

    private void Shoot()
    {
        if (shootingCD <= 0 && currentAmmo > 0)
        {
            shootingCD = 1 / shootsPS;
            currentAmmo--;

            //Shoot
            AmmoManager.instance.RemoveOneBulletFromAmmo(2);

          animator.SetTrigger("Shoot");
            AudioManager.instance.AkSfxShoot();
            pInteract.PlaceHole();
        }


    }
    private void Reload()
    {
        animator.SetTrigger("Reload");
        AudioManager.instance.ReloadSfx();
        currentAmmo = ammo;
        AmmoManager.instance.CreateAmmoCanvas(currentAmmo,  2);

    }
    private void Update()
    {
        shootingCD -= Time.deltaTime;
    }




}
