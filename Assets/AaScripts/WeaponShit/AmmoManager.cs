using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoManager : MonoBehaviour
{
    #region Vars
    #endregion
    #region SelfRunningMethods
    #endregion
    #region Private Methods
    #endregion
    #region public methods
    #endregion
    //AmmoShit
    [SerializeField] GameObject ammoVisual;
    [SerializeField] Transform firstAmmoSpot;
    [SerializeField] TextMeshProUGUI totalAmmoText;

    //PistolShit
    [SerializeField] List<GameObject> totalPistolAmmoList = new List<GameObject>();
    [SerializeField] List<GameObject> currentPistolAmmoList = new List<GameObject>();

    //AkShit
    [SerializeField] List<GameObject> totalAkAmmoList = new List<GameObject>();
    [SerializeField] List<GameObject> currentAkAmmoList = new List<GameObject>();

    //SniperShit
    [SerializeField] List<GameObject> totalSniperAmmoList = new List<GameObject>();
    [SerializeField] List<GameObject> currentSniperAmmoList = new List<GameObject>();

    private int distanceInAmmoHud = 10;


    public void CreateAmmoPool(int magazineAmmo, int ammoType)
    {
        for (int i = 0; i < magazineAmmo; i++)
        {

            switch (ammoType)
            {
                case 1:
                    Vector3 newPosition = new Vector3(firstAmmoSpot.transform.position.x + i * distanceInAmmoHud, firstAmmoSpot.transform.position.y, firstAmmoSpot.transform.position.z);
                    GameObject go = Instantiate(ammoVisual, newPosition, Quaternion.identity);
                    go.transform.SetParent(firstAmmoSpot.transform);
                    go.SetActive(false);
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
    public void CreateAmmoCanvas(int currentAmmo, int ammoType, int totalAmmo)
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

                case 3:


                    GameObject go3 = totalSniperAmmoList[i];
                    currentSniperAmmoList.Add(go3);
                    go3.SetActive(true);
                    break;
            }



        }
        UpdateTextAmmoHud(totalAmmo);
    }
    public void UpdateTextAmmoHud(int totalAmmo)
    {
        totalAmmoText.text = totalAmmo.ToString();

    }
    public void RemoveAllAmmoFromCanvas(int totalAmmo)
    {
        currentPistolAmmoList.Clear();
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
        UpdateTextAmmoHud(totalAmmo);
    }
    public void ReloadAmmo(int magazineTotalAmmo, int ammoType, int totalAmmo)
    {
        RemoveAllAmmoFromCanvas(magazineTotalAmmo);

        CreateAmmoCanvas(magazineTotalAmmo, ammoType, totalAmmo);

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
            case 3:

                if (currentSniperAmmoList.Count <= 0) return;
                GameObject go3 = currentSniperAmmoList[currentSniperAmmoList.Count - 1];
                currentSniperAmmoList.Remove(go3);
                go3.SetActive(false);
                break;
        }



    }
}

