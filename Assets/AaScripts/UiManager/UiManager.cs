using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using UnityEngine.UI;

public class UiManager : NetworkBehaviour
{
    /// <summary>
    /// UI manager used for player
    /// </summary>

    PlayerManager pManager;
    //points text
    [SerializeField] TextMeshProUGUI playerPoints;
    //used for buying items
    [SerializeField] GameObject priceHud;
    [SerializeField] TextMeshProUGUI priceText;
    //Hud parent, containing all other objs
    [SerializeField] GameObject hudGameObject;
    //hud to display player hp
    [SerializeField] Image healthHud;

    private void Awake()
    {
        pManager = GetComponent<PlayerManager>();
    }
    private void Start()
    {
        //only localplayers should see the hud
        if(!IsLocalPlayer) hudGameObject.SetActive(false);
    }
    //called from playerManager, on the playerpoints getter an setter(will update the points text)
    public void UpdatePlayerPoints(int newPoints)
    {
        if (!IsOwner) return;
        playerPoints.text = newPoints.ToString();
    }
    //show the price hud with the int pointsNeeded
    public void ShowPrice(int pointsNeeded)
    {
        if (!IsOwner) return;
        priceHud.SetActive(true);
        priceText.text = pointsNeeded.ToString();
    }
    //hide the price
    public void HidePrice()
    {
        if (!IsOwner) return;
        priceHud.SetActive(false);
    }


    public void SetHealthHud()
    {
        Color color = healthHud.color;
        float healthNormalized = pManager.PlayerHealth / 100f;

        color.a = 1f - healthNormalized;

        healthHud.color = color;
    }
}