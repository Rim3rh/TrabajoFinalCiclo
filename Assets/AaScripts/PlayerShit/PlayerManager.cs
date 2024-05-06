using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region VARS
    private UiManager uiManager;
    private void Awake()
    {
        uiManager = GetComponent<UiManager>();
    }
    public int PlayerPoints
    {
        get
        {
            return playerPoints;
        }
        set
        {
            playerPoints = value;
            //Disabled for now
             uiManager.UpdatePlayerPoints(playerPoints);
        }
    }
    //Will keep count of the points the player has tto spend
    private int playerPoints = 1000;
    //MouseSensitivity
    public float sensitivity;
    public float sensMultiplier;
    //Movement Variables
    public float playerSpeed;
    public float playerJumpForce;
    public bool playerSprint;
    //PlayerGroundedState
    public bool isPlayerGrounded;
    //CurrentInputs, used to manage animations without the need of accesing specific movement class
    public Vector2 playerCurrentInputs;

    public int playerHealth;
    #endregion


    private void Start()
    {
        uiManager.UpdatePlayerPoints(playerPoints);

    }
}
