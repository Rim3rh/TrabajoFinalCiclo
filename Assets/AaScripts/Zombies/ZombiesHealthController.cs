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
    //Zombie current Hp
    [HideInInspector] public float zombieHealth;
    //bool that will make the zombie be able to recive damage
    [HideInInspector] public bool canBoShoot;
    //reference to GameEvent scriptable object called when zombie dies)
    [SerializeField] GameEvent onZombieDeath;
    //private reference of go that will store the player zombie is the closest too
    private GameObject player;
    #endregion
    #region SelfRunningMethods
    private void Awake()
    {
        //Finding imediate references
        animController = GetComponent<ZombieAnimatorController>();
        wellInteractor = GetComponent<ZombieWellInteractor>();
    }
    #endregion
    #region public methods
    //will get called by IShooteable(Interface) when zombie is hit
    public void TakeDamge(float damage)
    {
        if (!canBoShoot) return;
        EnemyHitServerRpc(damage);
    }
    //Used to set player from other clases
    public void SetPlayer(GameObject player)
    {
        //player from this class = to reference from FindPlayer
        this.player = player;
    }
    #endregion
    #region Private Methods
    //Its serverRPC because its logic only the server should have, ownership set to false since clients will be calling it
    [ServerRpc(RequireOwnership = false)]
    private void EnemyHitServerRpc(float damage)
    {
        if (zombieHealth <= 0)
        {
            //killing method that will be happening on all clients
            EnemyDieClientRpc();
            //server will send onZombieDeath, server does it, so only server is aware of its death
            onZombieDeath.Raise();
            //check for particlesystem called when colse to a well.(Not called with onZombieDeathEvent, since its the same obj
            wellInteractor.OnZombieDeathPs();
            return;
        }
        else
        {
            zombieHealth -= damage;
            //hitting method that will be happening on all clients
            EnemyHitClientRpc();
        }
        
    }
    //All clients should see the enemy get hit and die.
    [ClientRpc]
    private void EnemyHitClientRpc()
    {
        //hit animation
        animController.Hit();
        //check for erros,although player will never be null
        if (player != null)
        {
            //give player points
            player.GetComponent<PlayerManager>().PlayerPoints += 10;
            //set player back to null
            player = null;
        }
    }
    [ClientRpc]
    private void EnemyDieClientRpc()
    {
        //play die animation
        animController.Die();
        //set well to null with delay so onzombieDeath event will take some time, and if well is = to null particles will not apear
        StartCoroutine(wellInteractor.CurrentWellToNull());
        //can shoot to false, since dead zombies should not recive damage
        canBoShoot = false;
        //check for erros,although player will never be null
        if (player != null)
        {
            //give player points
            player.GetComponent<PlayerManager>().PlayerPoints += 100;
            //set player back to null
            player = null;
        }
    }
    #endregion   
}
