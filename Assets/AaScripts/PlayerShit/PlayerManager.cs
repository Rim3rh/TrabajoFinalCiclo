using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //[SerializeField] UiManager uiManager;


    public int PlayerPoints
    {
        get
        {
            return playerPoints;
        }

        set 
        {
            playerPoints = value;
           // uiManager.UpdatePlayerPoints(playerPoints);


        }
    }
    private int playerPoints;







    public float sensitivity;
    public float sensMultiplier;
    public float playerSpeed;
    public float playerJumpForce;
    public bool isPlayerGrounded;
    public Vector2 playerCurrentInputs;
    public bool playerSprint;



    private void Awake()
    {
        PlayerPoints = 0;
    }


    
}
