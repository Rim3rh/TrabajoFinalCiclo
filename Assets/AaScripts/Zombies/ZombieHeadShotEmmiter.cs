using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHeadShotEmmiter : MonoBehaviour, IShooteable
{
    //class used to trasnmit headshots from head to parent obj
    [SerializeField] ZombiesHealthController healthController;
    //set the player how shot you
    public void SetPlayer(GameObject player)
    {
        healthController.SetPlayer(player);
    }
    //take the damage
    public void TakeDamge(float damage)
    {
        healthController.TakeDamge(damage * 2);
    }
}
