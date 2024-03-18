using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DummyManager : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;

    private Animator animator;
    private bool canBeHit;



    public bool beenHit;
    public bool canBeRaised;

    private void Awake()
    {
        canBeRaised = true;
        animator = GetComponent<Animator>();
    }
    public void HeadShot()
    {
        if (!canBeHit) return;

        animator.SetTrigger("Hit");
        //AudioManager.instance.EnemyHit();
        levelManager.hittedTargets++;
        levelManager.AddHeadShotPoints();
        canBeHit = false;
        beenHit = true;
    }

    public void BodyShoot()
    {
        if (!canBeHit) return;

        animator.SetTrigger("Hit");
        levelManager.hittedTargets++;
        levelManager.AddBodyShotPoints();
        canBeHit = false;
        beenHit = true;

    }



    private void CanBeHitToTrue()
    {
        canBeHit = true;
    }
    private void CanBeHitToFalse()
    {
        canBeHit = false;

    }
    private void CanBeRaisedToTrue()
    {
        canBeRaised = true;
    }
    private void BeenHitReset()
    {
        beenHit = false;
    }
}
