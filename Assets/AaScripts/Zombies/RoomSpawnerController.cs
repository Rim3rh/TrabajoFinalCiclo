using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnerController : MonoBehaviour
{
    //class reference
    [SerializeField] ZombieRoundManager roomRoundManager;
    //arrays storing all spawnpositions transforms
    [SerializeField] Transform[] room1SpawnPositions;
    [SerializeField] Transform[] room2SpawnPositions;
    [SerializeField] Transform[] room3SpawnPositions;
    public void AddRoomToSpawnPositions(int room)
    {
        //depending on the room you get, add the spawn positions
        switch (room)
        {
            case 1:
                foreach (Transform t in room1SpawnPositions)
                {
                    if (roomRoundManager.activeSpawnPositions.Contains(t)) return;
                    roomRoundManager.activeSpawnPositions.Add(t);
                }
                break;
            case 2:
                foreach (Transform t in room2SpawnPositions)
                {
                    if (roomRoundManager.activeSpawnPositions.Contains(t)) return;
                    roomRoundManager.activeSpawnPositions.Add(t);
                }
                break;
            case 3:
                foreach (Transform t in room3SpawnPositions)
                {
                    if (roomRoundManager.activeSpawnPositions.Contains(t)) return;
                    roomRoundManager.activeSpawnPositions.Add(t);
                }
                break;
        }


    }
    public void RemoveRoom1FromSpawnPositions(int room)
    {
        //same thing but removing them
        switch (room)
        {
            case 1:
                foreach (Transform t in room1SpawnPositions)
                {
                    roomRoundManager.activeSpawnPositions.Remove(t);
                }
                break;
            case 2:
                foreach (Transform t in room2SpawnPositions)
                {
                    roomRoundManager.activeSpawnPositions.Remove(t);
                }
                break;
            case 3:
                foreach (Transform t in room3SpawnPositions)
                {
                    roomRoundManager.activeSpawnPositions.Remove(t);
                }
                break;
        }

    }
}
