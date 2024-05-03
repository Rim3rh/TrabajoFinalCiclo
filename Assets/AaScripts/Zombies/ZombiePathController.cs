using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class ZombiePathController : NetworkBehaviour
{
    private List<GameObject> players = new List<GameObject>();
    private GameObject targetPlayer;

    private NavMeshAgent agent;
    ZombieAnimatorController animController;
    public bool canMove = true;
    public bool isStunned;
    public bool isMoving;

    private float attackTimer = 0f; // Timer to track how long the enemy is close to the player

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animController = GetComponent<ZombieAnimatorController>();  
    }

    void Update()
    {
        if (!IsServer) return;
        CheckIfCanAttackPlayer();
        FindClosestPlayer();

        if (isStunned)
        {
            agent.isStopped = true;
            return;
        }

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
        float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.transform.position);

        if (distanceToPlayer < 1f)
        {
            canMove = false;
            // Increment attack timer
            attackTimer += Time.deltaTime;
            // If the attack timer exceeds 0.15 seconds, trigger the attack animation
            if (attackTimer >= 0.15f)
            {
                animController.Attack();
            }
        }
        else
        {
            canMove = true;
            // Reset attack timer when the enemy is not close to the player
            attackTimer = 0f;
        }
    }
}
