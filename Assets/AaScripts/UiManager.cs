using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI playerPoints;
    [SerializeField] TextMeshProUGUI comboAmmount, comboMultiplier;

    private void Awake()
    {
        CleanCombos();
    }
    public void UpdatePlayerPoints(int newPoints)
    {
        playerPoints.text = newPoints.ToString();
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
}