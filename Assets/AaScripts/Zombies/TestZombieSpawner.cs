using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;

public class TestZombieSpawner : NetworkBehaviour
{
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] ZombiePoolManager poolManager;

    private void Start()
    {
        Invoke(nameof(SpawnTestZombie), 3);
    }

    public void SpawnTestZombie()
    {
        if (!IsServer) return;
        SpawnZombieServerRpc();
    }



    [ServerRpc]
    private void SpawnZombieServerRpc()
    {

        int randomPos = Random.Range(0, spawnPositions.Length);
        SpawnZombieClientRpc(randomPos);

    }


    [ClientRpc]
    private void SpawnZombieClientRpc(int randomPos)
    {
        //Get the zombie on the server
        GameObject zombie = poolManager.GetZombie();
        //Call the clientRPC
        //shit we want clients to see
        zombie.SetActive(true);
        zombie.GetComponent<Animator>().SetTrigger("Rise");
        //Set position
        zombie.transform.position = spawnPositions[randomPos].position;
        //La vida solo la necesita saber el server
        zombie.GetComponent<ZombiesHealthController>().zombieHealth = 5;
    }



}
