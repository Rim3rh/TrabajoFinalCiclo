using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Guns
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
        ammo = 1;
        shootsPS = 0.75f;
        currentAmmo = ammo;
        AmmoManager.instance.CreateAmmoPool(ammo, 3);
        AmmoManager.instance.CreateAmmoCanvas(currentAmmo, 3);
    }

    private void OnEnable()
    {

        pInteract.onShoot += Shoot;
        pInteract.onReload += Reload;
        AmmoManager.instance.CreateAmmoCanvas(currentAmmo, 3);

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
            AmmoManager.instance.RemoveOneBulletFromAmmo(3);

            animator.SetTrigger("Shoot");
            AudioManager.instance.PistolSfxShoot();
            pInteract.PlaceHole();

        }
        if (shootingCD <= 0 && currentAmmo <= 0)
        {
            shootingCD = 1 / shootsPS;
            AudioManager.instance.NoAmmoShoot();
        }

    }

    private void Reload()
    {
        animator.SetTrigger("Reload");
        AudioManager.instance.ReloadSfx();
        currentAmmo = ammo;
        AmmoManager.instance.CreateAmmoCanvas(currentAmmo, 3);


    }
    private void Update()
    {
        shootingCD -= Time.deltaTime;
    }
}
