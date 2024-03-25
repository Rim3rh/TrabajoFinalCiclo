using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class PlayerAnimationController : NetworkBehaviour
{
    PlayerManager pManager;
    [SerializeField] Animator cameraAnimator;
    [SerializeField] Animator weaponAnim;
    Animator bodyAnimator;
    private void Awake()
    {
        pManager = GetComponent<PlayerManager>();
        bodyAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!IsOwner) return;

        if (pManager.playerCurrentInputs.y > 0)
        {
            weaponAnim.SetBool("Walk", true);
            cameraAnimator.SetBool("Walk", true);
            bodyAnimator.SetBool("Walk", true);


        }
        else
        {
            cameraAnimator.SetBool("Walk", false);
            weaponAnim.SetBool("Walk", false);
            bodyAnimator.SetBool("Walk", false);


        }
        if(Mathf.Abs(pManager.playerCurrentInputs.x) > 0 && pManager.playerCurrentInputs.y !> 0 || pManager.playerCurrentInputs.y < 0)
        {
            bodyAnimator.SetBool("WalkBack", true);

        }
        else
        {
            bodyAnimator.SetBool("WalkBack", false);

        }




        if (pManager.playerSprint)
        {
            weaponAnim.SetFloat("WalkSpeed", 1.5f);
            cameraAnimator.SetFloat("WalkSpeed", 1.5f);

            cameraAnimator.SetBool("Run", true);
            bodyAnimator.SetBool("Run", true);


        }
        else
        {
            weaponAnim.SetFloat("WalkSpeed", 1);
            cameraAnimator.SetFloat("WalkSpeed", 1);

            cameraAnimator.SetBool("Run", false);
            bodyAnimator.SetBool("Run", false);


        }
    }


    public void Jump()
    {
        if (!IsOwner) return;

        cameraAnimator.SetTrigger("Jump");
        bodyAnimator.SetTrigger("Jump");

    }

    public void Land()
    {
        if (!IsOwner) return;

        cameraAnimator.SetTrigger("Land");

    }
}
