using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ZombiePoolManager : MonoBehaviour
{

    [SerializeField] GameObject zombiePrefab;

    List<GameObject> zombiePool = new List<GameObject>();

    List<GameObject> activeZombies = new List<GameObject>();

    [SerializeField] int zombiePoolSize;

    void Start()
    {
        for (int i = 0; i < zombiePoolSize; i++)
        {
            GameObject go = Instantiate(zombiePrefab, this.transform);
            zombiePool.Add(go);
            go.SetActive(false);


        }

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
