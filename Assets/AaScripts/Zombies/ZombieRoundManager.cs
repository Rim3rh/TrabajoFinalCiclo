using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class ZombieRoundManager : NetworkBehaviour
{
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] ZombiePoolManager poolManager;

    [SerializeField] ZombieRoundScriptableObject roundContainer;

    private List<ZombiesHealthController> zombiesToSpawn = new List<ZombiesHealthController>();

    [SerializeField] TextMeshProUGUI currentRoundText;

    public int currentRound = 1;


    public int ammountOfZombiesToSapwn;
    private float timer;
    bool inRound ;

    [SerializeField] GameEvent onRoundChanged;

    private void Start()
    {
        if(!IsServer) return;
        StartNewRound();
        currentRound  =1;
        UpdateCurrentRoundClientRpc(currentRound);

    }



    public void StartNewRound()
    {

        ammountOfZombiesToSapwn = roundContainer.rounds[currentRound].ammountOfZombies;
        timer = roundContainer.rounds[currentRound].spawnRate;
        inRound = true;
        onRoundChanged.Raise();


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


        if(poolManager.activeZombies.Count == 0 && inRound  && ammountOfZombiesToSapwn == 0)
        {
            inRound = false;
            currentRound++;
            Invoke(nameof(StartNewRound), 5f);
            UpdateCurrentRoundClientRpc(currentRound);
        }
    }



  

    public bool SpawnZombie()
    {
        if (poolManager.GetZombieChecker())
        {
            //si no es servidor ns que pasara la verdad xd
            SpawnZombieServerRpc();
            return true;
        }else return false;

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
        zombie.GetComponent<ZombiesHealthController>().zombieHealth = roundContainer.rounds[currentRound].zombiesHealth;
        if (!IsServer) zombie.GetComponent<Animator>().SetTrigger("Rise");

        zombie.GetComponent<NavMeshAgent>().speed = roundContainer.rounds[currentRound].zombiesSpeed;
    }

    [ClientRpc]
    public void UpdateCurrentRoundClientRpc(int round)
    {
        currentRoundText.text = round.ToString();
    }
}
