using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ZombiesHealthController : NetworkBehaviour, IShooteable
{
    #region Vars

    //Class References
    ZombieAnimatorController animController;
    //healthZombie has
    public float zombieHealth;

    public bool canBoShoot;

    [SerializeField] GameEvent onZombieDeath;
    #endregion

    private void Awake()
    {
        animController = GetComponent<ZombieAnimatorController>();
    }
    #region EnemyHit
    //will get called by canBeShoot when zombie is hit

    public void TakeDamge(float damage)
    {
        if (!canBoShoot) return;
        EnemyHitServerRpc(damage);
    }

    //Its serverRPC because its logic only the server should have.
    [ServerRpc(RequireOwnership = false)]
    private void EnemyHitServerRpc(float damage)
    {
        if (zombieHealth <= 0)
        {
            EnemyDieClientRpc();
            onZombieDeath.Raise();
            return;
        }
        else
        {
            zombieHealth-= damage;
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
