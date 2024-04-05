using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{

    #region Combo
    /*
    [SerializeField] TextMeshProUGUI comboAmmount, comboMultiplier;
    private void Awake()
    {
        CleanCombos();
    }
    public void UpdateCombos(int comboAmmountI, int multiplierI)
    {
        comboAmmount.text = comboAmmountI.ToString();
        comboMultiplier.text = multiplierI.ToString();
    }
    public void CleanCombos()
    {
        comboAmmount.text = " ";
        comboMultiplier.text = " ";
    }
    */
    #endregion
    [SerializeField] TextMeshProUGUI playerPoints;
    [SerializeField] GameObject priceHud;
    [SerializeField] TextMeshProUGUI priceText;

    public void UpdatePlayerPoints(int newPoints)
    {
        playerPoints.text = newPoints.ToString();
    }




    public void ShowPrice(int pointsNeeded)
    {
        priceHud.SetActive(true);
        priceText.text = pointsNeeded.ToString();
    }
    public void HidePrice()
    {
        priceHud.SetActive(false);
    }
}