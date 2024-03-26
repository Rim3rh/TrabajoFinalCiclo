using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class PlayerAnimationController : NetworkBehaviour
{
    #region VARS
    //Class references
    PlayerManager pManager;
    //Animator references
    [SerializeField] Animator cameraAnim;
    [SerializeField] Animator weaponAnim;
    Animator bodyAnim;
    #endregion
    #region SelfRunMethods
    private void Awake()
    {
        pManager = GetComponent<PlayerManager>();
        bodyAnim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!IsOwner) return;

        WalkAnimations();
        SprintingAnims();

    }
    #endregion
    #region PrivateMethods
    private void SprintingAnims()
    {
        if (pManager.playerSprint)
        {
            //Turn walkSpeed up, so the animation runs faster
            weaponAnim.SetFloat("WalkSpeed", 1.5f);
            cameraAnim.SetFloat("WalkSpeed", 1.5f);
            //Run anim for the ext body
            bodyAnim.SetBool("Run", true);
        }
        else
        {
            weaponAnim.SetFloat("WalkSpeed", 1);
            cameraAnim.SetFloat("WalkSpeed", 1);

            bodyAnim.SetBool("Run", false);
        }
    }
    private void WalkAnimations()
    {
        //Walking forward
        if (pManager.playerCurrentInputs.y > 0)
        {
            weaponAnim.SetBool("Walk", true);
            cameraAnim.SetBool("Walk", true);
            bodyAnim.SetBool("Walk", true);
        }
        else
        {
            cameraAnim.SetBool("Walk", false);
            weaponAnim.SetBool("Walk", false);
            bodyAnim.SetBool("Walk", false);
        }

        //WalkingBack
        if (Mathf.Abs(pManager.playerCurrentInputs.x) > 0 && pManager.playerCurrentInputs.y! > 0 || pManager.playerCurrentInputs.y < 0)
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
        if (!IsOwner) return;

        cameraAnim.SetTrigger("Jump");
        bodyAnim.SetTrigger("Jump");

    }
    #endregion
}
