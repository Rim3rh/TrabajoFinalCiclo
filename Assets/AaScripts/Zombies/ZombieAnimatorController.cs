//using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class ZombieAnimatorController : NetworkBehaviour
{
    #region Vars
    //class references
    ZombiesHealthController healthController;
    ZombiePathController pathController;
    //Component References
    Animator anim;
    #endregion
    #region SelfRunningMethods
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
    #endregion
    #region Private Methods
    private void WalkAnimation()
    {
        //call animation on clients giving bool from server(server knows when zombie moves but client dosent)
        WalkAnimationClientRpc(pathController.isMoving);
    }
    [ClientRpc]
    private void WalkAnimationClientRpc(bool isMoving)
    {
        //aply bool on animator depending on ismoving bool
        if (isMoving)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }

    [ClientRpc]
    private void HitClientRpc()
    {
        //animation call for clients
        anim.SetTrigger("Hit");
    }
    [ClientRpc]
    private void DieClientRpc()
    {
        //animation call for clients
        anim.SetTrigger("Die");
    }
    [ClientRpc]
    private void AttackClientRpc()
    {
        //animation call for clients
        anim.SetTrigger("Attack");
    }
    #endregion
    #region public methods
    //public methods called from other scripts in zombie
    public void Hit()
    {
        HitClientRpc();
    }
    public void Die()
    {
        DieClientRpc();
    }
    public void Attack()
    {
        AttackClientRpc();
    }
    #endregion
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
