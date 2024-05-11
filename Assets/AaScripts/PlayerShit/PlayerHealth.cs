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
        manager.playerHealth = 100;
    }
    private void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKeyDown(KeyCode.O))
        {
            RevivePlayer();
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
        if (manager.playerHealth <= 0)
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
        AdjustBodyViewsClientRpc();
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
        manager.playerHealth -= damage;
        CheckForDeath();
        StartCoroutine(TakeDamageHud());
    }
    public void KillPlayer()
    {
        Debug.Log("Kill player " + OwnerClientId);

        if (manager.isDead) return;
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
        Debug.Log("Revive player" + OwnerClientId);

        if (!manager.isDead) return;
        SwitchActionMap("PlayerNormalMovement");
        //set pos to 000
        transform.position = Vector3.zero;
        //prio back to normal
        virtualCamera.Priority = 10;
        deadHud.SetActive(false);
        RevivePlayerServerRpc();
        AdjustBodyViewsRevivePlayerClientRpc();
        manager.isDead = false;
        manager.playerHealth = 100;
    }
    #endregion




}
