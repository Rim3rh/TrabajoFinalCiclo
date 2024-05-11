using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetector : MonoBehaviour
{
    [Tooltip("The room where the spawners are.")]
    [SerializeField] int room;
    [Tooltip("Reference to spawn controller class.")]
    [SerializeField] RoomSpawnerController spawnerController;
    //local var to determine the ammount of players in a room(used to enable/disable spawnpositions)
    private int ammountOfPlayersInRoom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ammountOfPlayersInRoom++;
            //if one player is in the room, add the spawnPositions
            spawnerController.AddRoomToSpawnPositions(room);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ammountOfPlayersInRoom--;
            //if there are 0 players,remove spawners, else dont.
            if (ammountOfPlayersInRoom == 0) spawnerController.RemoveRoom1FromSpawnPositions(room);
        }
    }


}
