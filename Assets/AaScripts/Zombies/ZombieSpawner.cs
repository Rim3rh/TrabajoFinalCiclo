using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TestZombieSpawner : NetworkBehaviour
{
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] ZombiePoolManager poolManager;

    private void Start()
    {
        if (!IsServer) return;
    }



    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            SpawnZombie(spawnPositions[Random.Range(0, spawnPositions.Length)]);
        }
    }
    private void SpawnZombie(Transform spanwpos)
    {
        GameObject zombie = poolManager.GetZombie();
        zombie.SetActive(true);
        zombie.transform.position = spanwpos.position;
        zombie.GetComponent<ZombiesHealthController>().zombieHealth = 5;
        zombie.GetComponent<Animator>().SetTrigger("Rise");
        GetComponent<NetworkObject>().Spawn();
    }



}
