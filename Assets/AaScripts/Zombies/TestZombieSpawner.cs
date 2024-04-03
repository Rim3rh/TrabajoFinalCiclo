using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TestZombieSpawner : NetworkBehaviour
{
    [SerializeField] Transform spawnPos;
    [SerializeField] ZombiePoolManager poolManager;

    private void Start()
    {
        if (!IsServer) return;
        Invoke(nameof(SpawnZombie), 10f);
    }


    private void SpawnZombie()
    {
        GameObject zombie = poolManager.GetZombie();
        zombie.SetActive(true);
        zombie.transform.position = spawnPos.position;
        zombie.GetComponent<ZombiesHealthController>().zombieHealth = 5;
        zombie.GetComponent<Animator>().SetTrigger("Rise");
        GetComponent<NetworkObject>().Spawn();
    }



}
