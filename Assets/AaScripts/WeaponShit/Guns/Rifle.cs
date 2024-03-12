using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rifle : Guns
{

    Animator animator;
    private float shootingCD;
    private int currentAmmo;


    //AmmoShit
    [SerializeField] GameObject ammoPicture;
    [SerializeField] Transform firstAmmoSpot;
    [SerializeField] List<GameObject> ammoList = new List<GameObject>();

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        ammo = 30;
        shootsPS = 4;
        currentAmmo = ammo;
    }

    private void OnEnable()
    {
        WeaponManager.onShoot += Shoot;
        CreateAmmoCanvas();
    }

    private void OnDisable()
    {
        WeaponManager.onShoot -= Shoot;
        RemoveAmmoFromCanvas();
    }

    private void Shoot()
    {
        if (shootingCD <= 0 && currentAmmo >= 0)
        {
            shootingCD = 1 / shootsPS;
            currentAmmo--;
            ammoList.RemoveAt(ammoList.Count -1);

            //Shoot
            animator.SetTrigger("Shoot");
        }


    }

    private void Update()
    {
        shootingCD -= Time.deltaTime;
    }



    private void CreateAmmoCanvas()
    {
        for (int i = 0; i < ammo; i++)
        {
            GameObject go = ammoPicture;
            Vector3 newPosition = new Vector3(firstAmmoSpot.transform.position.x + i * 3, firstAmmoSpot.transform.position.y, firstAmmoSpot.transform.position.z);
            Instantiate(go, newPosition, Quaternion.identity);
            ammoList.Add(go);
        }
    }
    private void RemoveAmmoFromCanvas()
    {
        foreach (GameObject go in ammoList)
        {
            go.SetActive(false);
            ammoList.Remove(go);

        }
    }
}
