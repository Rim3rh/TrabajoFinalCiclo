using System.Collections;
using System.Collections.Generic;
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
        zombie.GetComponent<ZombieAnimatorController>().zombieHealth = 5;
        zombie.GetComponent<Animator>().SetTrigger("Rise");
    }
}
