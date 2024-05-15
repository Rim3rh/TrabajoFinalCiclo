using Cinemachine;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : NetworkBehaviour
{

    #region Vars
    //Playermanager reference
    private PlayerManager manager;
    //Input action asset
    [SerializeField] InputActionAsset inputActions;
    //PlayerCamera
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    //Hud displayed when dead
    [SerializeField] GameObject deadHud;
    [SerializeField] GameObject hitHud;

    //health regeneration
    bool hasBeenHit;
    float hasBeenHitTimer;
    #endregion
    #region SelfRunningMethods
    private void Awake()
    {
        //Getting direcct referenmces
        manager = GetComponent<PlayerManager>();
        //We look for gameendchecker here, cause on start, because of the delay it wont find it
        
    }
    private void Start()
    {
        if (!IsOwner) return;
        //Set player health
        manager.PlayerHealth = 100;
    }
    private void Update()
    {
        if (!IsOwner) return;
        //if you have been hit, start counting seconds to reheal
        if (hasBeenHit)
        {
            hasBeenHitTimer += Time.deltaTime;
            //if you reach 2 seconds withou getting hit, heal 25
            if(hasBeenHitTimer >= 2)
            {
                manager.PlayerHealth += 25;
                //if u are full hp, your no loger hit, if ur not, set tiumer to 1 since it should take less timne to heal rest hp
                if (manager.PlayerHealth == 100) hasBeenHit = false;
                else hasBeenHitTimer = 1;
            }
        }

    }

    #endregion
    #region Private Methods
    [ServerRpc(RequireOwnership = false)]
    private void KillPlayerServerRpc()
    {
        Debug.Log("server kills player " + OwnerClientId);

        //server updates current aliveplayers, so it knows when to end game
        GameObject.FindObjectOfType<GameEndChecker>().KillOnePlayer();
        //set bodylayers correct for all players
        AdjustBodyViewsClientRpc();
    }
    [ClientRpc]
    private void AdjustBodyViewsClientRpc()
    {
        //tell clients to do it for all players, includiung the ones theyu dont controll
        foreach (BodyLayerSetter gameObject in GameObject.FindObjectsOfType<BodyLayerSetter>())
        {
            gameObject.DethCamera();
        }
    }
    private void CheckForDeath()
    {
        //kill player
        if (manager.PlayerHealth <= 0)
        {
            KillPlayer();
        }
    }
    [ServerRpc(RequireOwnership = false)]
    private void RevivePlayerServerRpc()
    {
        Debug.Log("Server revives player" + OwnerClientId);

        //tell server to revive one player on the logic
        GameObject.FindObjectOfType<GameEndChecker>().ReviveOnePlayer();
        //adjust the body layers backwords
        AdjustBodyViewsRevivePlayerClientRpc();
    }
    [ClientRpc]
    private void AdjustBodyViewsRevivePlayerClientRpc()
    {
        //adjust layers back to normal
        foreach (BodyLayerSetter gameObject in GameObject.FindObjectsOfType<BodyLayerSetter>())
        {
            gameObject.BackToNormalCamera();
        }
    }
    void SwitchActionMap(string actionMapName)
    {
        if (!IsOwner) return;
        //Find map to set
        InputActionMap actionMapToEnable = inputActions.FindActionMap(actionMapName);

        if (actionMapToEnable != null)
        {
            // Disable all other action maps
            foreach (var map in inputActions.actionMaps)
            {
                if (map != actionMapToEnable)
                {
                    map.Disable();
                }
            }
            actionMapToEnable.Enable();
        }
    }

    private IEnumerator TakeDamageHud()
    {
        hitHud.SetActive(true);
        yield return new WaitForSeconds(.15f);
        hitHud.SetActive(false);
    }
    #endregion
    #region public methods
    //Methods zombies wil call
    public void TakeDamge(int damage)
    {
        if (!IsLocalPlayer) return;
        //rehealing shit
        hasBeenHit = true;
        hasBeenHitTimer = 0f;

        manager.PlayerHealth -= damage;
        CheckForDeath();
        StartCoroutine(TakeDamageHud());
    }
    private void KillPlayer()
    {
        if (manager.isDead) return;
        //healinng shit
        hasBeenHit = false;
        hasBeenHitTimer = 0f;

        manager.PlayerHealth = 100;
        deadHud.SetActive(true);
        KillPlayerServerRpc();
        SwitchActionMap("Dead");
        //trasnport player to deadposition
        transform.position = GameObject.FindGameObjectWithTag("DeadPos").transform.position;
        //change priority so player sees trhought other players cam
        virtualCamera.Priority = 0;
        manager.isDead = true;

    }
    public void RevivePlayer()
    {
        if (!manager.isDead) return;
        SwitchActionMap("PlayerNormalMovement");
        //set pos to 000
        transform.position = Vector3.zero;
        //prio back to normal
        virtualCamera.Priority = 10;
        deadHud.SetActive(false);
        RevivePlayerServerRpc();
        manager.isDead = false;
        manager.PlayerHealth = 100;
    }
    #endregion




}
