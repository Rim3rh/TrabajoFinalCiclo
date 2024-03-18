using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ZombieAnimatorController : NetworkBehaviour
{
    Animator animator;


    [SerializeField] BeatManager beatManager;

    private bool canWalk;
    private bool isWalking;

    private void Start()
    {
        isWalking = false;
    }
    private void Awake()
    {
        
        animator = GetComponent<Animator>();
    }
    public void EnemyHit()
    {
        if(!IsOwner) return;
        EnemyHitServerRpc();
    }


    [ServerRpc(RequireOwnership = false)]
    private void EnemyHitServerRpc()
    {
        EnemyHitClientRpc();
    }
    [ClientRpc]
    private void EnemyHitClientRpc()
    {
        Debug.Log("HITENEMIG");
        animator.SetTrigger("Hit");
        isWalking = false;
        canWalk = false;
    }



    private void Update()
    {
        if (!IsServer) return;

        if (canWalk)
        {
            if (beatManager.coordinationTrigger)
            {
                isWalking = true;
            }
        }

        WalkAnimServerRpc();

    }

    [ServerRpc]
    private void WalkAnimServerRpc()
    {
        WalkAnimClientRpc();
    }
    [ClientRpc]
    private void WalkAnimClientRpc()
    {
        if (isWalking)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);

        }
    }




    private void CanWalkToTrue()
    {
                canWalk = true;

    }
}
