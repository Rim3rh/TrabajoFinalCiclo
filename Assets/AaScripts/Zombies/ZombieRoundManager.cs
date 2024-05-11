using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class ZombieRoundManager : NetworkBehaviour
{
    #region Vars
    //Class references
    [SerializeField] ZombiePoolManager poolManager;
    [SerializeField] ZombieRoundScriptableObject roundContainer;
    //List that will keep all active spawn positions(spawn pos where zombies can spawn)
    [HideInInspector] public List<Transform> activeSpawnPositions = new List<Transform>();
    //List containing zombiestospawn
    private List<ZombiesHealthController> zombiesToSpawn = new List<ZombiesHealthController>();
    //canvas text to display rounds
    [SerializeField] TextMeshProUGUI currentRoundText;

    int currentRound = 1;
    int ammountOfZombiesToSapwn;
    float timer;
    bool inRound;
    //game event reference so we can Raise it(let other classes klnow we have changed round)
    [SerializeField] GameEvent onRoundChanged;
    #endregion
    #region SelfRunningMethods
    private void Start()
    {
        if (!IsServer) return;
        StartNewRound();
        currentRound = 1;
        //this will update the current round canvas on all clients
        UpdateCurrentRoundClientRpc(currentRound);
    }
    private void Update()
    {
        if (!IsServer) return;
        if (inRound && ammountOfZombiesToSapwn > 0)
        {
            //si estas en ronda y tienes zombies que spawnear, resta al timer de spawnrate
            timer -= Time.deltaTime;
            if (timer < 0)
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
        //si no hay zombies activos, estas en ronda, y no quedan zombies por spawnear pasas de ronda
        if (poolManager.activeZombies.Count == 0 && inRound && ammountOfZombiesToSapwn == 0)
        {
            inRound = false;
            currentRound++;
            Invoke(nameof(StartNewRound), 5f);
            UpdateCurrentRoundClientRpc(currentRound);
        }
    }
    #endregion
    #region Private Methods
    [ClientRpc]
    private void OnRoundChangedRaiseClientRpc()
    {
        //raise the event
        onRoundChanged.Raise();
    }
    [ServerRpc]
    private void SpawnZombieServerRpc()
    {
        //to spawn the zombie, we get the spanwpos in the server, since we want client zombies t spawn in same pos,
        int randomPos = UnityEngine.Random.Range(0, activeSpawnPositions.Count);
        //then we spawn it and pass the random spawnpos
        SpawnZombieClientRpc(randomPos);
    }
    [ClientRpc]
    private void SpawnZombieClientRpc(int randomPos)
    {
        //Get the zombie
        GameObject zombie = poolManager.GetZombie();
        //if the zombie == null, means server and client dissagre on the solution of GetZombie(), in that case, remove the last zombie on server and retry
        if (zombie == null)
        {
            //this part i think is only reached by clients
            if (!IsClient)
            {
                Debug.LogError("No hay zombie en el server");
                return;
            }
            else
            {
                RemoveLastZombieServerRpc();
                return;

            }
        }
        zombie.SetActive(true);
        //Set position
        zombie.transform.position = activeSpawnPositions[randomPos].position;
        //La vida solo la necesita saber el server
        zombie.GetComponent<ZombiesHealthController>().zombieHealth = roundContainer.rounds[currentRound].zombiesHealth;
        zombie.GetComponent<Animator>().SetTrigger("Rise");
        //add zombie to activeZombieList
        poolManager.activeZombies.Add(zombie);
        zombie.GetComponent<NavMeshAgent>().speed = roundContainer.rounds[currentRound].zombiesSpeed;
    }
    [ClientRpc]
    private void UpdateCurrentRoundClientRpc(int round)
    {
        //update text
        currentRoundText.text = round.ToString();
    }
    [ServerRpc(RequireOwnership = false)]
    private void RemoveLastZombieServerRpc()
    {
        poolManager.RemoveLastZombieFromList();
        timer = 0;
        ammountOfZombiesToSapwn++;
    }
    #endregion
    #region public methods
    public bool SpawnZombie()
    {
        //chek if we can get a zombie
        if (poolManager.GetZombieChecker())
        {
            //if you can, spawn it
            SpawnZombieServerRpc();
            return true;
        }
        else return false;
    }
    public void StartNewRound()
    {
        //get info from roundcontainer scriptable object
        ammountOfZombiesToSapwn = roundContainer.rounds[currentRound].ammountOfZombies;
        timer = roundContainer.rounds[currentRound].spawnRate;
        inRound = true;
        //raise onround chenged event on all clients
        OnRoundChangedRaiseClientRpc();
    }
    #endregion
}
