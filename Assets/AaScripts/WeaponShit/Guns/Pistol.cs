using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Guns
{

    Animator animator;
    private float shootingCD;
    private int currentAmmo;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        ammo = 6;
        shootsPS = 1;
        currentAmmo = ammo;
    }

    private void OnEnable()
    {
        WeaponManager.onShoot += Shoot;
    }

    private void OnDisable()
    {
        WeaponManager.onShoot -= Shoot;

    }

    private void Shoot()
    {
        if (shootingCD <= 0 && currentAmmo >= 0)
        {
            shootingCD = 1 / shootsPS;
            currentAmmo--;

            //Shoot
            animator.SetTrigger("Shoot");
        }
        

    }

    private void Update()
    {
        shootingCD -= Time.deltaTime;
    }
}
