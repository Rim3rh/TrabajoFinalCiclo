//using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ZombieAnimatorController : NetworkBehaviour
{
     //class references
     ZombiesHealthController healthController;
    //Component References
    Animator anim;

    private void Awake()
    {
        //Getting Components
        anim = GetComponent<Animator>();
        healthController = GetComponent<ZombiesHealthController>();
    }


    public void Hit()
    {
        anim.SetTrigger("Hit");
    }

    public void Die()
    {
        anim.SetTrigger("Die");
       
    }
    private void SetGoToInactive()
    {
        gameObject.SetActive(false);
    }


    private void CanBeShootToTrue()
    {
        healthController.canBoShoot = true;
    }
    private void CanBeShootToFalse()
    {
        healthController.canBoShoot=false;
    }

}
