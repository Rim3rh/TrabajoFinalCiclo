using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    //AmmoShit
    [SerializeField] GameObject pistolAmmo, akAmmo;

    public static AmmoManager instance;

    [SerializeField] Transform firstAmmoSpot;
    [SerializeField] List<GameObject> totalPistolAmmoList = new List<GameObject>();
    [SerializeField] List<GameObject> currentPistolAmmoList = new List<GameObject>();


    [SerializeField] List<GameObject> totalAkAmmoList = new List<GameObject>();
    [SerializeField] List<GameObject> currentAkAmmoList = new List<GameObject>();




    private bool firstPistol;


    private void Awake()
    {
        instance = this;
    }


    public void CreateAmmoPool(int totalAmmo, int ammoType)
    {
        for (int i = 0; i < totalAmmo; i++)
        {

            switch (ammoType)
            {
                case 1:
                    Vector3 newPosition = new Vector3(firstAmmoSpot.transform.position.x + i * 15, firstAmmoSpot.transform.position.y, firstAmmoSpot.transform.position.z);
                    GameObject go = Instantiate(pistolAmmo, newPosition, Quaternion.identity);
                    go.transform.SetParent(firstAmmoSpot.transform);
                    go.SetActive(false);
                    totalPistolAmmoList.Add(go);
                    break;

                case 2:

                    Vector3 newPosition2 = new Vector3(firstAmmoSpot.transform.position.x + i * 15, firstAmmoSpot.transform.position.y, firstAmmoSpot.transform.position.z);
                    GameObject go2 = Instantiate(akAmmo, newPosition2, Quaternion.identity);
                    go2.transform.SetParent(firstAmmoSpot.transform);
                    go2.SetActive(false);
                    totalAkAmmoList.Add(go2);
                    break;
            }



        }
    }
    public void CreateAmmoCanvas(int currentAmmo, int ammoType)
    {


        for (int i = 0; i < currentAmmo; i++)
        {

            switch (ammoType)
            {
                case 1:

                    GameObject go = totalPistolAmmoList[i];
                    currentPistolAmmoList.Add(go);
                    go.SetActive(true);

                    break;

                case 2:


                    GameObject go2 = totalAkAmmoList[i];
                    currentAkAmmoList.Add(go2);
                    go2.SetActive(true);
                    break;
            }



        }
    }
    public void RemoveAllAmmoFromCanvas(int totalAmmo)
    {
        currentPistolAmmoList.Clear();
        foreach( GameObject go in totalPistolAmmoList)
        {
            if(go.activeSelf) go.SetActive(false);

        }


        currentAkAmmoList.Clear ();
        foreach (GameObject go in totalAkAmmoList)
        {
            if (go.activeSelf) go.SetActive(false);
        }


    }


    public void RemoveOneBulletFromAmmo(int ammoType)
    {
        switch (ammoType)
        {
            case 1:
                if (currentPistolAmmoList.Count <= 0) return;

                GameObject go = currentPistolAmmoList[currentPistolAmmoList.Count - 1];
                currentPistolAmmoList.Remove(go);
                go.SetActive(false);
                break;

            case 2:

                if (currentAkAmmoList.Count <= 0) return;
                GameObject go2 = currentAkAmmoList[currentAkAmmoList.Count - 1];
                currentAkAmmoList.Remove(go2);
                go2.SetActive(false);
                break;
        }



    }
}

