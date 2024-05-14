using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ZombiePoolManager : NetworkBehaviour
{
    #region Vars
    [Tooltip("ZombiePrefab that will get spawned")]
    [SerializeField] GameObject zombiePrefab;
    [Tooltip("Ammount of zombies that are allowed to spawn at the same time")]
    [SerializeField] int zombiePoolSize;
    //list of the zombiePool created on start
    List<GameObject> zombiePool = new List<GameObject>();
    //list of zombies on scene
    public List<GameObject> activeZombies = new List<GameObject>();
    #endregion
    #region SelfRunningMethods
    void Start()
    {
        if (!IsServer) return;
        //Creating the pool only on the server
        CreateZombiePoolServerRpc();
    }
    #endregion
    #region Private Methods
    [ServerRpc]
    void CreateZombiePoolServerRpc()
    {
        //create the amount of zombies specified on zombiePoolSize
        for (int i = 0; i < zombiePoolSize; i++)
        {
            GameObject go = Instantiate(zombiePrefab, this.transform);
            //networkObjects need to be spawned
            go.GetComponent<NetworkObject>().Spawn();
        }
        //actions we want to happen on clients
        CreatePoolClientRpc();
    }
    [ClientRpc]
    void CreatePoolClientRpc()
    {
        //add all zombies on scene to the zombiePool list and set to inactive
        ZombiesHealthController[] zombies = ZombiesHealthController.FindObjectsOfType<ZombiesHealthController>();
        foreach (ZombiesHealthController zombie in zombies)
        {
            zombiePool.Add(zombie.gameObject);
            zombie.gameObject.SetActive(false);
        }
    }
    #endregion
    #region public methods
    public bool GetZombieChecker()
    {
        //this method is created so we can check if we have a zombie before creating it
        //check if there is any inactive zombie in zombiepool
        foreach (GameObject go in zombiePool)
        {
            if (!go.activeSelf)
            {
                return true;
            }

        }
        return false;
    }
    public GameObject GetZombie()
    {
        //check if there is any inactive zombie in zombiepool, if we have one return it
        foreach (GameObject go in zombiePool)
        {
            if (!go.activeSelf)
            {
                return go;
            }

        }
        Debug.LogWarning("Me has pedido zombie y no hay zombie huevon");
        return null;

    }
    //used to remove last zombie spawn in case client and server do not agree on zombie spawn
    public void RemoveLastZombieFromList()
    {
        activeZombies[activeZombies.Count - 1].SetActive(false);
        activeZombies.RemoveAt(activeZombies.Count - 1);
    }
    //Used for when you kill zombies(called on inspector)
    public void RemoveOneZombieFromList()
    {
        activeZombies.RemoveAt(activeZombies.Count - 1);
    }
    #endregion
}
