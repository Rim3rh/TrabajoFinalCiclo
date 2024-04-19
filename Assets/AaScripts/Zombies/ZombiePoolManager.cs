using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ZombiePoolManager : NetworkBehaviour
{

    [SerializeField] GameObject zombiePrefab;

    List<GameObject> zombiePool = new List<GameObject>();

    List<GameObject> activeZombies = new List<GameObject>();

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
        foreach (GameObject go in zombiePool)
        {
            if (!go.activeSelf)
            {
                return true;
            }

        }
        Debug.LogError("Me has pedido zombie y no hay zombie huevon");
        return false;

    }

    public GameObject GetZombie()
    {
        foreach (GameObject go in zombiePool)
        {
            if (!go.activeSelf)
            {
                activeZombies.Add(go);
                return go;
            }

        }
        Debug.LogError("Me has pedido zombie y no hay zombie huevon");
        return null;

    }
    public void DisableZombie(GameObject zombieToDesable)
    {
        activeZombies.Remove(zombieToDesable);
        zombieToDesable.SetActive(false);
    }

}
