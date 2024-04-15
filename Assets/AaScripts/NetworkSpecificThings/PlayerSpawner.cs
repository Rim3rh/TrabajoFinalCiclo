using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField] GameObject playerPrefab;

    private int spawnPos;
    [SerializeField] Transform [] spawnPositions;
    private void Start()
    {
        if (!IsServer) return;
        foreach(ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            GameObject player = Instantiate(playerPrefab);
            player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
            player.transform.position = spawnPositions[spawnPos].position;
            spawnPos++;
        }
    }


}
