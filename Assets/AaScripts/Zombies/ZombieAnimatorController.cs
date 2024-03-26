//using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ZombieAnimatorController : NetworkBehaviour
{
    //Component References
    Animator animator;


    [SerializeField] BeatManager beatManager;

    private bool canWalk;
    private bool isWalking = false;
    public int zombieHealth;
    private void Awake()
    {
        //Getting Components
        animator = GetComponent<Animator>();
    }
    public void EnemyHit()
    {
        EnemyHitServerRpc();
    }


    [ServerRpc(RequireOwnership = false)]
    private void EnemyHitServerRpc()
    {
        
        isWalking = false;
        canWalk = false;
        if (zombieHealth <= 0)
        {
            EnemyDieClientRpc();
            return;
        }
        else
        {
            zombieHealth--;
        }
        EnemyHitClientRpc();
    }
    [ClientRpc]
    private void EnemyHitClientRpc()
    {
        animator.SetTrigger("Hit");


    }
    [ClientRpc]
    private void EnemyDieClientRpc()
    {
        animator.SetTrigger("Die");
        gameObject.SetActive(false);
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

    [ServerRpc(RequireOwnership = false)]
    private void WalkAnimServerRpc()
    {
        if (isWalking)
        {
            WalkAnimToTrueClientRpc();
        }
        else
        {
            WalkAnimToFalseClientRpc();

        }
    }
    [ClientRpc]
    private void WalkAnimToTrueClientRpc()
    {
        animator.SetBool("Walk", true);


    }

    [ClientRpc]
    private void WalkAnimToFalseClientRpc()
    {
        animator.SetBool("Walk", false);

    }


    private void CanWalkToTrue()
    {
                canWalk = true;

    }
}
