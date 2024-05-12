using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoManager : MonoBehaviour
{
    #region Vars
    //Ammo sprite displayed in hud
    [SerializeField] GameObject ammoVisual;
    //where the ammo hud will start to generate
    [SerializeField] Transform firstAmmoSpot;
    //distance in betwen ammo sprites
    private int distanceInAmmoHud = 10;
    //text displayting total ammo
    [SerializeField] TextMeshProUGUI totalAmmoText;

    //lists to keep track of ammo sprites, and ammo to be displayed
    //PistolShit
    [SerializeField] List<GameObject> totalPistolAmmoList = new List<GameObject>();
    [SerializeField] List<GameObject> currentPistolAmmoList = new List<GameObject>();

    //AkShit
    [SerializeField] List<GameObject> totalAkAmmoList = new List<GameObject>();
    [SerializeField] List<GameObject> currentAkAmmoList = new List<GameObject>();

    //SniperShit
    [SerializeField] List<GameObject> totalSniperAmmoList = new List<GameObject>();
    [SerializeField] List<GameObject> currentSniperAmmoList = new List<GameObject>();
    #endregion
    #region public methods
    //this method will create the pool of ammo sprites for each weapon
    public void CreateAmmoPool(int magazineAmmo, int ammoType)
    {
        for (int i = 0; i < magazineAmmo; i++)
        {
            //depending on the ammotype(in case weapon share ammo) create ammo pool
            switch (ammoType)
            {
                case 1:
                    //set the possition with fristammo spot and multipliying by i ^distance so it is farther away each intineration
                    Vector3 newPosition = new Vector3(firstAmmoSpot.transform.position.x + i * distanceInAmmoHud, firstAmmoSpot.transform.position.y, firstAmmoSpot.transform.position.z);
                    //create the obj
                    GameObject go = Instantiate(ammoVisual, newPosition, Quaternion.identity);
                    //give it its parent(so we have it organized)
                    go.transform.SetParent(firstAmmoSpot.transform);
                    //set it to inactive so we cant see it untill we need it
                    go.SetActive(false);
                    //add it to the list
                    totalPistolAmmoList.Add(go);
                    break;

                case 2:

                    Vector3 newPosition2 = new Vector3(firstAmmoSpot.transform.position.x + i * distanceInAmmoHud, firstAmmoSpot.transform.position.y, firstAmmoSpot.transform.position.z);
                    GameObject go2 = Instantiate(ammoVisual, newPosition2, Quaternion.identity);
                    go2.transform.SetParent(firstAmmoSpot.transform);
                    go2.SetActive(false);
                    totalAkAmmoList.Add(go2);
                    break;

                case 3:

                    Vector3 newPosition3 = new Vector3(firstAmmoSpot.transform.position.x + i * distanceInAmmoHud, firstAmmoSpot.transform.position.y, firstAmmoSpot.transform.position.z);
                    GameObject go3 = Instantiate(ammoVisual, newPosition3, Quaternion.identity);
                    go3.transform.SetParent(firstAmmoSpot.transform);
                    go3.SetActive(false);
                    totalSniperAmmoList.Add(go3);
                    break;
            }
        }
        
    }
    //make the ammo actually visible
    public void CreateAmmoCanvas(int currentAmmo, int ammoType, int totalAmmo)
    {
        //create the ammo canvas with the current ammo, that way if you need to reaload, ammo is displayed correctly
        for (int i = 0; i < currentAmmo; i++)
        {
            //depenginf on the ammo type, we use different sprites
            switch (ammoType)
            {
                case 1:
                    //go trhouhg the list
                    GameObject go = totalPistolAmmoList[i];
                    //add it to the currentammo list
                    currentPistolAmmoList.Add(go);
                    //enable it
                    go.SetActive(true);

                    break;

                case 2:


                    GameObject go2 = totalAkAmmoList[i];
                    currentAkAmmoList.Add(go2);
                    go2.SetActive(true);
                    break;

                case 3:


                    GameObject go3 = totalSniperAmmoList[i];
                    currentSniperAmmoList.Add(go3);
                    go3.SetActive(true);
                    break;
            }



        }
        //update the total ammo text in the hud
        UpdateTextAmmoHud(totalAmmo);
    }
    public void UpdateTextAmmoHud(int totalAmmo)
    {
        totalAmmoText.text = totalAmmo.ToString();
    }
    //used to disable all ammo sprites
    public void RemoveAllAmmoFromCanvas()
    {
        //clear the current ammo list(becazuse you are going to disable all obj now)
        currentPistolAmmoList.Clear();
        //for each go in the list, disable it(we dont remove it from the list, since this is the pool, not the current list
        foreach( GameObject go in totalPistolAmmoList)
        {
            if(go.activeSelf && go != null) go.SetActive(false);
        }
        currentAkAmmoList.Clear ();
        foreach (GameObject go in totalAkAmmoList)
        {
            if (go.activeSelf && go != null) go.SetActive(false);
        }
        currentSniperAmmoList.Clear();
        foreach (GameObject go in totalSniperAmmoList)
        {
            if (go.activeSelf && go != null) go.SetActive(false);
        }
    }
    //for realoading, we first remove all ammo sprites, then generate new ones with reloaded info done on the weapon logic.
    public void ReloadAmmo(int magazineTotalAmmo, int ammoType, int totalAmmo)
    {
        RemoveAllAmmoFromCanvas();
        CreateAmmoCanvas(magazineTotalAmmo, ammoType, totalAmmo);
    }
    //called when shot, removes one bullet from list(last one)
    public void RemoveOneBulletFromAmmo(int ammoType)
    {
        //needed to know what weapon ur using
        switch (ammoType)
        {
            case 1:
                //if you dont have ammo retunr
                if (currentPistolAmmoList.Count <= 0) return;
                //find last obj in the list
                GameObject go = currentPistolAmmoList[currentPistolAmmoList.Count - 1];
                //remove it from the list and disable it
                currentPistolAmmoList.Remove(go);
                go.SetActive(false);
                break;

            case 2:

                if (currentAkAmmoList.Count <= 0) return;
                GameObject go2 = currentAkAmmoList[currentAkAmmoList.Count - 1];
                currentAkAmmoList.Remove(go2);
                go2.SetActive(false);
                break;
            case 3:

                if (currentSniperAmmoList.Count <= 0) return;
                GameObject go3 = currentSniperAmmoList[currentSniperAmmoList.Count - 1];
                currentSniperAmmoList.Remove(go3);
                go3.SetActive(false);
                break;
        }



    }
    #endregion
}

