using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class ZombiePathController : MonoBehaviour
{
    private List<GameObject> players = new List<GameObject>();
    private GameObject targetPlayer;

    private NavMeshAgent agent;

    public bool canMove = true;
    public bool isMoving;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        CheckIfCanAttackPlayer();
        FindClosestPlayer();
        if (canMove)
        {
            if (targetPlayer != null)
            {
                FollowPlayer();
            }
        }
        else
        {
            isMoving = false;
            CancelFollowPlayer();
        }

    }
    private void OnEnable()
    {
        FindCurrentPlayers();
    }

    private void FollowPlayer()
    {
        isMoving = true;
        agent.destination = targetPlayer.transform.position;
        agent.isStopped = false;    

    }
    private void CancelFollowPlayer()
    {
        agent.isStopped = true;
    }

    private void FindClosestPlayer()
    {
        float minDistance = Mathf.Infinity;
        GameObject closestPlayer = null;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPlayer = player;
            }
        }

        targetPlayer = closestPlayer;
    }

    private void FindCurrentPlayers()
    {
        players.Clear(); 
        foreach (PlayerManager player in GameObject.FindObjectsOfType<PlayerManager>())
        {
            players.Add(player.gameObject);
        }
    }

    private void CheckIfCanAttackPlayer()
    {
        if (targetPlayer == null) return;
        if(Vector3.Distance(this.transform.position, targetPlayer.transform.position) < 1f)
        {
            canMove = false;
            Debug.Log("AHNDSAOIJHDOAS^PJNDM");
        }
        else
        {
            canMove = true;
        }
    }
}
