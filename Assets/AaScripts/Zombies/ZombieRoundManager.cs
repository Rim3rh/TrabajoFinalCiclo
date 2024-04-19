using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ZombieRoundManager : NetworkBehaviour
{
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] ZombiePoolManager poolManager;



    TestZombieSpawner zombieSpawner;

    [SerializeField] ZombieRoundScriptableObject roundContainer;

    private List<ZombiesHealthController> zombiesToSpawn = new List<ZombiesHealthController>();


    public int currentRound = 1;


    private int ammountOfZombiesToSapwn;
    private float timer;
    bool inRound ;


    private void Start()
    {
        StartNewRound();
        currentRound  =1;
    }


    public void StartNewRound()
    {

        ammountOfZombiesToSapwn = roundContainer.rounds[currentRound].ammountOfZombies;
        timer = roundContainer.rounds[currentRound].spawnRate;
        inRound = true;


    }

    private void Update()
    {
        if (!IsServer) return;

        if (inRound && ammountOfZombiesToSapwn > 0)
        {
            //si estas en ronda y tienes zombies que spawnear, resta al timer de spawnrate
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                //cunado llegue a 0, intentas spawnear un zombie, si devuelve treue, significa q lo ha spawneado
                if (SpawnZombie())
                {
                    timer = roundContainer.rounds[currentRound].spawnRate;
                    ammountOfZombiesToSapwn--;
                }
                //sino, lo seguira intentando hasta q spawne
            }

        }
    }



  

    public bool SpawnZombie()
    {
        //si no es servidor ns que pasara la verdad xd
        SpawnZombieServerRpc();
        return poolManager.GetZombieChecker();
    }



    [ServerRpc]
    //declaro metodo como bool para poder detectar si ha habido un fallo a la hora de conseguir un zombie
    private void SpawnZombieServerRpc()
    {

        int randomPos = UnityEngine.Random.Range(0, spawnPositions.Length);
        SpawnZombieClientRpc(randomPos);

    }


    [ClientRpc]
    //declaro metodo como bool para poder detectar si ha habido un fallo a la hora de conseguir un zombie
    private void SpawnZombieClientRpc(int randomPos)
    {
        //Get the zombie on the server
        GameObject zombie = poolManager.GetZombie();
        if (zombie == null)
        {

            return;
        }
        zombie.SetActive(true);

        //Set position
        zombie.transform.position = spawnPositions[randomPos].position;
        //La vida solo la necesita saber el server
        zombie.GetComponent<ZombiesHealthController>().zombieHealth = 5;
        if (!IsServer) zombie.GetComponent<Animator>().SetTrigger("Rise");
    }


}
