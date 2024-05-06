using Cinemachine;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
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
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            KillPlayer();
        }
    }


    private void KillPlayer()
    {
        KillPlayerServerRpc();
        SwitchActionMap("Dead");
        transform.position = GameObject.FindGameObjectWithTag("DeadPos").transform.position;
        virtualCamera.Priority = 0;
        deadHud.SetActive(true);

    }
    [ServerRpc()]
    private void KillPlayerServerRpc()
    {
        GameObject.FindObjectOfType<GameEndChecker>().KillOnePlayer();
        AdjustBodyViewsClientRpc();
    }
    [ClientRpc]
    private void AdjustBodyViewsClientRpc()
    {
        bodyLayerSetter.DethCamera();
    }
    private void CheckForDeath()
    {
        if(manager.playerHealth <= 0)
        {

            KillPlayer();
        }
    }
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
