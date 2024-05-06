using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetector : MonoBehaviour
{

    [SerializeField] int room;

    [SerializeField] RoomSpawnerController spawnerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ammountOfPlayersInRoom++;
            spawnerController.AddRoomToSpawnPositions(room);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ammountOfPlayersInRoom--;
            if(ammountOfPlayersInRoom == 0) spawnerController.RemoveRoom1FromSpawnPositions(room);
        }
    }


    [SerializeField] int ammountOfPlayersInRoom;
}
