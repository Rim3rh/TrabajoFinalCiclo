using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombiePathController : MonoBehaviour
{
    private List<GameObject> players = new List<GameObject>();
    private GameObject targetPlayer;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        FindCurrentPlayers();
    }

    void Update()
    {
        FindClosestPlayer();
        if (targetPlayer != null)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        agent.destination = targetPlayer.transform.position;
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

    private void OnEnable()
    {
        FindCurrentPlayers();
    }

    private void FindCurrentPlayers()
    {
        players.Clear(); 
        foreach (PlayerManager player in GameObject.FindObjectsOfType<PlayerManager>())
        {
            players.Add(player.gameObject);
        }
    }
}
