using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public bool inGame;

    [SerializeField] TextMeshPro score;
    [SerializeField] float timeToShoot;
    [SerializeField] float timeBetweenDummies;
    [SerializeField] int totalAmmountOfDummies;
    private int ammountOfDummies;

    public float hittedTargets;


    [SerializeField] GameObject[] dummies;


    public void StartGame()
    {
        InvokeRepeating(nameof(DummyActivator), 0.1f, timeBetweenDummies);
        inGame = true;
    }


    private void DummyActivator()
    {
        if(ammountOfDummies == totalAmmountOfDummies) EndGame();
        if (!inGame)
        {
            CancelInvoke();
            return;
        }
        GameObject dummy = dummies[Random.Range(0, dummies.Length)];
        while(!dummy.GetComponent<DummyManager>().canBeRaised) dummy = dummies[Random.Range(0, dummies.Length)];
        Animator animator = dummy.GetComponent<Animator>();
        DummyManager manager = dummy.GetComponent<DummyManager>();

        manager.canBeRaised = false;
        animator.SetTrigger("Rise");
        manager.beenHit = false;
        StartCoroutine(TimeToShootCorrutine(animator));
        ammountOfDummies++;
    }
    private IEnumerator TimeToShootCorrutine(Animator anim)
    {
        DummyManager manager = anim.GetComponent<DummyManager>();

        if (!inGame)
        {
            StopAllCoroutines();
            yield break;
        }
        yield return new WaitForSeconds(timeToShoot);
        if (manager.beenHit) yield break;

        anim.SetTrigger("GoBack");
    }



    private void EndGame()
    {
        inGame = false;
        score.text = hittedTargets.ToString() + "/" +totalAmmountOfDummies.ToString();
    }
}
