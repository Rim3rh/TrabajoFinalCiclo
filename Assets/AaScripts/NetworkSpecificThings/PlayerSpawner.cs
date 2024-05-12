using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSpawner : NetworkBehaviour
{
    GameEndChecker gameEndChecker;
    //reference to playerPrefab
    [SerializeField] GameObject playerPrefab;
    //spawnpos count
    private int spawnPos;
    //array for spawn positions
    [SerializeField] Transform [] spawnPositions;
    private void Awake()
    {
        gameEndChecker = GetComponent<GameEndChecker>();
    }
    private void Start()
    {
        //only server will spawn players
        if (!IsServer) return;
        //loop done for each player in the connectedPlayerIds
        foreach(ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            //create player
            GameObject player = Instantiate(playerPrefab);
            //spawn as player on the network
            player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
            gameEndChecker.alivePlayers++;
            //set its position
            player.transform.position = spawnPositions[spawnPos].position;
            //add to the int so next player spawns in different pos
            spawnPos++;
        }
    }
}
