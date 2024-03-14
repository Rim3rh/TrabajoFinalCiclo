using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimatorController : MonoBehaviour
{
    Animator animator;


    [SerializeField] BeatManager beatManager;

    private bool canWalk;
    private bool isWalking;

    private void Start()
    {
        isWalking = false;
    }
    private void Awake()
    {
        
        animator = GetComponent<Animator>();
    }
    public void EnemyHit()
    {
        animator.SetTrigger("Hit");
        isWalking = false;
        canWalk = false;

    }


    private void Update()
    {
        if (canWalk)
        {
            if (beatManager.coordinationTrigger)
            {
                isWalking = true;
            }
        }

        if(isWalking)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);

        }
    }


    private void CanWalkToTrue()
    {
                canWalk = true;

    }
}
