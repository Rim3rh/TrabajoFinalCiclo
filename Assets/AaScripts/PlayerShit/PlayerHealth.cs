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
    private PlayerManager manager;
    BodyLayerSetter bodyLayerSetter;
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] GameObject deadHud;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
        bodyLayerSetter = GetComponent<BodyLayerSetter>();
    }
    public void TakeDamge(int damage)
    {
        manager.playerHealth -= damage;
        CheckForDeath();
    }
    private void Update()
    {
        if (!IsOwner) return;
        if(Input.GetKeyDown(KeyCode.P)) 
        {
            KillPlayer();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            RevivePlayer();
        }
    }

    #region KillPlayer
    public void KillPlayer()
    {
        if (manager.isDead) return;
        deadHud.SetActive(true);
        KillPlayerServerRpc();
        SwitchActionMap("Dead");
        transform.position = GameObject.FindGameObjectWithTag("DeadPos").transform.position;
        virtualCamera.Priority = 0;
        manager.isDead = true;

    }
    [ServerRpc(RequireOwnership = false)]
    private void KillPlayerServerRpc()
    {
        GameObject.FindObjectOfType<GameEndChecker>().KillOnePlayer();
        AdjustBodyViewsClientRpc();
    }
    [ClientRpc]
    private void AdjustBodyViewsClientRpc()
    {
        Debug.Log("SOY ESTE");
        foreach (BodyLayerSetter gameObject in GameObject.FindObjectsOfType<BodyLayerSetter>())
        {
            gameObject.DethCamera();
        }
    }
    private void CheckForDeath()
    {
        if(manager.playerHealth <= 0)
        {

            KillPlayer();
        }
    }
    #endregion
    #region RevivePlayer

    public void RevivePlayer()
    {
        if (!manager.isDead) return;
        SwitchActionMap("PlayerNormalMovement");
        transform.position = Vector3.zero;
        virtualCamera.Priority = 10;
        deadHud.SetActive(false);
        RevivePlayerServerRpc();
        AdjustBodyViewsRevivePlayerClientRpc();
        manager.isDead = false;

    }
    [ServerRpc(RequireOwnership = false)]
    private void RevivePlayerServerRpc()
    {
        GameObject.FindObjectOfType<GameEndChecker>().ReviveOnePlayer();
        AdjustBodyViewsClientRpc();
    }
    [ClientRpc]
    private void AdjustBodyViewsRevivePlayerClientRpc()
    {
        Debug.Log("SOY ESTE");
        foreach (BodyLayerSetter gameObject in GameObject.FindObjectsOfType<BodyLayerSetter>())
        {
            gameObject.BackToNormalCamera();
        }
    }

    #endregion
    void SwitchActionMap(string actionMapName)
    {
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
}
