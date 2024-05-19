using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class WellParticleSpawner : NetworkBehaviour
{
    #region Vars
    [Tooltip("particle system used for zombies death")]
    [SerializeField] ParticleSystem particleSystem;
    [Tooltip("reference to gameManager")]
    [SerializeField] GameEndChecker gameEndChecker;
    //particle system used when well is completed
    [SerializeField] ParticleSystem endParticleSystem;
    [Tooltip("souls needed to complete soul")]
    [SerializeField] int maxAmmountOfSouls;
    //private reference to ammount of souls on current well
    int ammountOfSouls;
    //bool used to stop souls from spwaning
    public bool isWellCompleted;
    #endregion
    #region Private Methods
    [ClientRpc]
    private void SpawnClientRpc(float posX, float posY, float posZ)
    {
        //spawn it, play it and destroy it
        ParticleSystem ps = Instantiate(particleSystem);
        if(IsServer) ps.GetComponent<NetworkObject>().Spawn();
        ps.transform.position = new Vector3(posX, posY, posZ);
        ps.Play();
        Destroy(ps, 2f);
    }

    [ClientRpc]
    private void EndWellClientRpc()
    {
        endParticleSystem.Play();
    }

    #endregion
    #region public methods
    //public method called from zombies when die
    public void SpawnNewParticle(Vector3 positionToSpawn)
    {
        //this will be run on clients
        if (isWellCompleted) return;
        //call client rpc with zombie position where to spawn particle system
        SpawnClientRpc(positionToSpawn.x, positionToSpawn.y, positionToSpawn.z);
    }
    public void CheckIfWellCompleted()
    {
        //this will be run only on server
        ammountOfSouls++;
        Debug.Log("PARTICLE HERE");
        //if souls completed reporduce ps and tell gameendchecjer well is completed
        if (ammountOfSouls >= maxAmmountOfSouls)
        {
            isWellCompleted = true;
            gameEndChecker.OneWellCompleted();
            EndWellClientRpc();
        }
    }
    #endregion
}
