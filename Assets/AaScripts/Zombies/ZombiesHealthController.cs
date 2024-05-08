using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ZombiesHealthController : NetworkBehaviour, IShooteable
{
    #region Vars

    //Class References
    ZombieAnimatorController animController;
    ZombieWellInteractor wellInteractor;

    //healthZombie has
    public float zombieHealth;

    public bool canBoShoot;

    [SerializeField] GameEvent onZombieDeath;

    private GameObject player;
    #endregion

    private void Awake()
    {
        animController = GetComponent<ZombieAnimatorController>();
        wellInteractor = GetComponent<ZombieWellInteractor>();
    }
    #region EnemyHit
    //will get called by canBeShoot when zombie is hit

    public void TakeDamge(float damage)
    {
        if (!canBoShoot) return;
        EnemyHitServerRpc(damage);
    }
    public void FindPlayer(GameObject player)
    {
        this.player = player;
    }
    //creating this so head can pass a player in case you dont have one
    public void SetPlayer(GameObject player)
    {
        this.player = player;

    }
    //Its serverRPC because its logic only the server should have.
    [ServerRpc(RequireOwnership = false)]
    private void EnemyHitServerRpc(float damage)
    {
        if (zombieHealth <= 0)
        {
            EnemyDieClientRpc();
            onZombieDeath.Raise();
            wellInteractor.OnZombieDeathPs();
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
        if (player != null)
        {
            player.GetComponent<PlayerManager>().PlayerPoints += 10;
            player = null;
        }


    }
    [ClientRpc]
    private void EnemyDieClientRpc()
    {
        animController.Die();
        if (player != null)
        {
            player.GetComponent<PlayerManager>().PlayerPoints += 100;
            player = null;
        }
    }
    #endregion
}
