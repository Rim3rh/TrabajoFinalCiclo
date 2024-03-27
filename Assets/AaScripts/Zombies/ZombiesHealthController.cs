using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ZombiesHealthController : NetworkBehaviour
{
    #region Vars

    //Class References
    ZombieAnimatorController animController;
    //healthZombie has
    public int zombieHealth;

    public bool canBoShoot;
    #endregion

    private void Awake()
    {
        animController = GetComponent<ZombieAnimatorController>();
    }
    #region EnemyHit
    //will get called by canBeShoot when zombie is hit
    public void EnemyHit()
    {
        if (!canBoShoot) return;
        EnemyHitServerRpc();
    }
    //Its serverRPC because its logic only the server should have.
    [ServerRpc(RequireOwnership = false)]
    private void EnemyHitServerRpc()
    {
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
    //All clients should see the enemy get hit and die.
    [ClientRpc]
    private void EnemyHitClientRpc()
    {
        animController.Hit();
    }
    [ClientRpc]
    private void EnemyDieClientRpc()
    {
        animController.Die();

    }
    #endregion
}
