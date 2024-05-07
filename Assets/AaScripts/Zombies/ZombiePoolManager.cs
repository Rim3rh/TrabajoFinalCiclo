using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ZombiePoolManager : NetworkBehaviour
{

    [SerializeField] GameObject zombiePrefab;

    List<GameObject> zombiePool = new List<GameObject>();

    public List<GameObject> activeZombies = new List<GameObject>();

    [SerializeField] int zombiePoolSize;

    

    void Start()
    {
        if (!IsServer) return;
        CreateZombiePoolServerRpc();

    }


    [ServerRpc]
    void CreateZombiePoolServerRpc()
    {
        for (int i = 0; i < zombiePoolSize; i++)
        {
            GameObject go = Instantiate(zombiePrefab, this.transform);
            go.GetComponent<NetworkObject>().Spawn();
        } 
        CreatePoolClientRpc();

    }

    [ClientRpc]

    void CreatePoolClientRpc()
    {
        ZombiesHealthController[] zombies = ZombiesHealthController.FindObjectsOfType<ZombiesHealthController>();
        foreach (ZombiesHealthController zombie in zombies)
        {
            zombiePool.Add(zombie.gameObject);
            zombie.gameObject.SetActive(false);
        }
    }



    public bool GetZombieChecker()
    {
        // when you kill a zombnie, because of the delay, the server knows it has an inactive one, but client dosent,
        //so zombie is spawned on server but not on clietn(i think)

        //solution: on zombieRoundManager, im going to try to revertspawn if client gives an error on spawn
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
    public void RemoveOneZombieFromList()
    {
        activeZombies.RemoveAt(activeZombies.Count-1);
    }
    public void RemoveLastZombieFromList()
    {
        activeZombies[activeZombies.Count - 1].SetActive(false);

        activeZombies.RemoveAt(activeZombies.Count - 1);
    }
}
