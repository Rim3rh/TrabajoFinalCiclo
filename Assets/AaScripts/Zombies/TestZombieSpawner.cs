using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TestZombieSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPos;
    [SerializeField] ZombiePoolManager poolManager;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnZombie();
        }
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
