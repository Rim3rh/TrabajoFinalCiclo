//using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class ZombieAnimatorController : NetworkBehaviour
{
     //class references
     ZombiesHealthController healthController;
    ZombiePathController pathController;
    //Component References
    Animator anim;

    private void Awake()
    {
        //Getting Components
        anim = GetComponent<Animator>();
        healthController = GetComponent<ZombiesHealthController>();
        pathController = GetComponent<ZombiePathController>();
    }

    private void Update()
    {
        WalkAnimation();
    }


    private void WalkAnimation()
    {
        if (pathController.isMoving)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }
    public void Hit()
    {
        anim.SetTrigger("Hit");
    }

    public void Die()
    {
        anim.SetTrigger("Die");
       
    }


    #region References For Animator
    private void CanMoveToTrue()
    {
        pathController.canMove = true;
    }
    private void CanMoveToFalse()
    {
        pathController.canMove= false;
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
        healthController.canBoShoot = false;
    }
    #endregion
}
