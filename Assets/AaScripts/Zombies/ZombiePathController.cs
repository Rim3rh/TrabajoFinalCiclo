using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class ZombiePathController : NetworkBehaviour
{
    #region Vars
    //reference to anim controller
    ZombieAnimatorController animController;
    //zombieAgent
    private NavMeshAgent zombieAgent;
    //find all players in scene
    private List<GameObject> players = new List<GameObject>();
    //closest player
    private GameObject targetPlayer;
    //made public to be modified from other classes(animator controller)
    public bool canMove = true;
    public bool isStunned;
    public bool isMoving;
    public bool isAttacking;
    //timer to controll zombie attack
    float attackTimer;
    #endregion
    #region SelfRunningMethods
    private void Awake()
    {
        //getting class references
        zombieAgent = GetComponent<NavMeshAgent>();
        animController = GetComponent<ZombieAnimatorController>();
    }
    void Update()
    {
        //logic only done on serverr
        if (!IsServer) return;
        //find closest player of list of players
        FindClosestPlayer();
        //check for attack
        CheckIfCanAttackPlayer();
        //look at player when attacking so it dosent look wierd
        if (isAttacking) LookAtPlayer();
        //check when to stop agent
        if (isStunned || isAttacking)
        {
            CancelFollowPlayer();
            return;
        }
        //move player
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
    #endregion
    #region Private Methods
    private void LookAtPlayer()
    {
        if (targetPlayer != null)
        {
            //Calculate lookrotation
            Quaternion targetRotation = Quaternion.LookRotation(targetPlayer.transform.position - transform.position);
            //ignore all axis but y
            transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y,0);
        }
    }
    private void FollowPlayer()
    {
        isMoving = true;
        zombieAgent.destination = targetPlayer.transform.position;
        zombieAgent.isStopped = false;
    }
    private void CancelFollowPlayer()
    {
        zombieAgent.isStopped = true;
    }
    private void FindClosestPlayer()
    {
        //calculate closest player of all players in list
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
        //clear list
        players.Clear();
        foreach (PlayerManager player in GameObject.FindObjectsOfType<PlayerManager>())
        {
            //add plauyers to list
            players.Add(player.gameObject);
        }
    }
    private void CheckIfCanAttackPlayer()
    {
        if (targetPlayer == null) return;
        //calculate distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.transform.position);
        if (distanceToPlayer < 1.5f && !isAttacking && !isStunned)
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
        if (distanceToPlayer > 1.5f && !isAttacking && !isStunned)
        {
            canMove = true;
            // Reset attack timer when the enemy is not close to the player
            attackTimer = 0f;
        }
    }
    //called from animator
    public void CheckZombieDamage()
    {
        //this is called when animation hit from zombie hits
        if (targetPlayer != null)
        {
            //check if your close to player
            float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.transform.position);

            if (distanceToPlayer < 1.5f)
            {
                targetPlayer.GetComponent<PlayerHealth>().TakeDamge(25);
            }
        }
    }
    #endregion
}
