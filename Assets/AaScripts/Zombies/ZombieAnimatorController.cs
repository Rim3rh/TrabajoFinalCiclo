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
        WalkAnimationClientRpc(pathController.isMoving);
    }
    [ClientRpc]
    private void WalkAnimationClientRpc(bool isMoving)
    {
        if (isMoving)
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
        HitClientRpc();
    }
    [ClientRpc]
    private void HitClientRpc()
    {
        anim.SetTrigger("Hit");

    }

    public void Die()
    {
        DieClientRpc();
    }
    [ClientRpc]
    private void DieClientRpc()
    {
        anim.SetTrigger("Die");

    }
    public void Attack()
    {
        AttackClientRpc();
    }
    [ClientRpc]
    private void AttackClientRpc()
    {
        anim.SetTrigger("Attack");

    }


    #region References For Animator
    private void CanMoveToTrue()
    {
        pathController.isStunned = false;
    }
    private void CanMoveToFalse()
    {
        pathController.isStunned = true;
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
    private void StartAttack()
    {
        pathController.isAttacking = true;
        pathController.canMove = false;
    }
    private void EndAttack()
    {
        pathController.isAttacking = false;
        pathController.canMove = true;
    }
    #endregion
}
