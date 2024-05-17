using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class PlayerAnimationController : NetworkBehaviour
{
    #region VARS
    //Class references
    PlayerManager pManager;
    //Animator references
    [SerializeField] Animator weaponAnim;
    Animator bodyAnim;
    ClientNetworkAnimator networkBodyAnim;
    #endregion
    #region SelfRunMethods
    private void Awake()
    {
        //Getting components from player
        pManager = GetComponent<PlayerManager>();
        bodyAnim = GetComponent<Animator>();
        networkBodyAnim = GetComponent<ClientNetworkAnimator>();
    }
    private void Update()
    {
        //Animations will only be run by owner
        if (!IsOwner) return;

        WalkAnimations();
        SprintingAnims();

    }
    #endregion
    #region Private Methods
    private void SprintingAnims()
    {
        if (pManager.playerSprint && pManager.playerCurrentInputs.y > 0)
        {
            //Turn walkSpeed up, so the animation runs faster
            weaponAnim.SetFloat("WalkSpeed", 1.5f);
            //Run anim for the ext body
            bodyAnim.SetBool("Run", true);
        }
        else
        {
            weaponAnim.SetFloat("WalkSpeed", 1);

            bodyAnim.SetBool("Run", false);
        }
    }
    private void WalkAnimations()
    {
        //Walking forward
        if (pManager.playerCurrentInputs.y > 0)
        {
            weaponAnim.SetBool("Walk", true);
            bodyAnim.SetBool("Walk", true);
        }
        else
        {
            weaponAnim.SetBool("Walk", false);
            bodyAnim.SetBool("Walk", false);
        }

        //WalkingBack
        if (Mathf.Abs(pManager.playerCurrentInputs.x) > 0 && pManager.playerCurrentInputs.y !>= 0 || pManager.playerCurrentInputs.y < 0)
        {
            bodyAnim.SetBool("WalkBack", true);

        }
        else
        {
            bodyAnim.SetBool("WalkBack", false);

        }
    }
    public void Jump()
    {
        //using networkbodyanim cause normal animator was not working, you can onyl use this for triggers
        networkBodyAnim.SetTrigger("Jump");
    }
    #endregion

    #region AnimatorReferences
    private void PlayWalkSfx()
    {
        AudioManager.instance.PlayerWalkSfx(this.transform.position);
    }
    #endregion

}
