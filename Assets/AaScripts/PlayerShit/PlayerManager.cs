using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region VARS
    //reference to uiManager
    private UiManager uiManager;
    //player points getter/setter
    public int PlayerPoints
    {
        get
        {
            //if you ask for the points, you get the points
            return playerPoints;
        }
        set
        {
            //if you set the points, adjust its value, then update the player points HUD
            playerPoints = value;
             uiManager.UpdatePlayerPoints(playerPoints);
        }
    }
    private int playerPoints = 1000;
    //MouseSensitivity and its multiplier
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

    //weapon states
    public bool isReloading;
    public bool isChangingWeapon;

    //game end states
    public int playerHealth;
    public bool isDead;
    #endregion
    private void Awake()
    {
        //getting reference(from same go)
        uiManager = GetComponent<UiManager>();
    }
    private void Start()
    {
        //update the points on start
        uiManager.UpdatePlayerPoints(PlayerPoints);
    }
}
