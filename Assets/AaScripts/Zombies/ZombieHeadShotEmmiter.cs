using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHeadShotEmmiter : MonoBehaviour, IShooteable
{
    [SerializeField] ZombiesHealthController healthController;
    public void FindPlayer(GameObject player)
    {
        healthController.SetPlayer(player);
    }

    public void TakeDamge(float damage)
    {
        healthController.TakeDamge(damage * 2);
    }


}
