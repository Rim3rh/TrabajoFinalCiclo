using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class UiManager : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI playerPoints;
    [SerializeField] GameObject priceHud;
    [SerializeField] TextMeshProUGUI priceText;

    [SerializeField] GameObject hudGameObject;




    private void Start()
    {
        if(!IsLocalPlayer) hudGameObject.SetActive(false);
    }
    public void UpdatePlayerPoints(int newPoints)
    {
        if (!IsOwner) return;
        playerPoints.text = newPoints.ToString();
    }


    public void ShowPrice(int pointsNeeded)
    {
        if (!IsOwner) return;
        priceHud.SetActive(true);
        priceText.text = pointsNeeded.ToString();
    }
    public void HidePrice()
    {
        if (!IsOwner) return;
        priceHud.SetActive(false);
    }
}