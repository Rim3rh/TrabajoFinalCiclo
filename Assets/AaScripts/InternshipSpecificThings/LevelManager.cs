using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public bool inGame;

    [SerializeField] GameObject player;


    [SerializeField] TextMeshPro score;
    [SerializeField] float timeToShoot;
    [SerializeField] float timeBetweenDummies;
    [SerializeField] int totalAmmountOfDummies;
    private int ammountOfDummies;

    public float hittedTargets;


    [SerializeField] GameObject[] dummies;




    [SerializeField] bool endless;

    public void StartGame()
    {
        Invoke(nameof(DummyActivator), 0.1f);
        score.text = hittedTargets.ToString() + "/" ;
        ammountOfDummies = 0;
        hittedTargets = 0;
        inGame = true;
    }


    private void DummyActivator()
    {
        Invoke(nameof(DummyActivator), timeBetweenDummies);

        if (endless)
        {
            if (timeToShoot > 1.5f) timeToShoot -= 0.05f;
            timeBetweenDummies -= 0.05f;
        }
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
        //AudioManager.instance.EnemyMove();

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
        if (manager.beenHit ) yield break;
        anim.SetTrigger("GoBack");
        if(endless) EndGame();
        //AudioManager.instance.EnemyMove();

    }


    private void EndGame()
    {
        inGame = false;
        score.text = hittedTargets.ToString() + "/" +totalAmmountOfDummies.ToString();
        if(endless) score.text = hittedTargets.ToString() + "/" + "∞";

    }



    public void AddHeadShotPoints()
    {
        player.GetComponent<PlayerManager>().PlayerPoints += 100;
    }
    public void AddBodyShotPoints()
    {
        player.GetComponent<PlayerManager>().PlayerPoints += 50;
    }
}
