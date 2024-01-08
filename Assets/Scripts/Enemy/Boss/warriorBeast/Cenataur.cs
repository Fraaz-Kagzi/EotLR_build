using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.AI;

public class Cenataur : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    bool isPerformingSpecialAttack;
    int normalAttacksCount = 0;

    private Guardian guard; // Reference to the Ork script


    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;


    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;



    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (guard != null && guard.health <= 0)
        {

            playerInAttackRange = true;
            playerInSightRange = false;
            agent.SetDestination(transform.position);
            Debug.Log("dead");
        }

        if (!playerInSightRange && !playerInAttackRange)
        {

            GetComponent<Animator>().SetBool("isAttacking", false);
            GetComponent<Animator>().SetBool("isMoving", true);
            Patroling();


        }
        else if (playerInSightRange && !playerInAttackRange)
        {
            GetComponent<Animator>().SetBool("isAttacking", false);
            GetComponent<Animator>().SetBool("isMoving", true);

            
                ChasePlayer();
            
        }
       /* else if (playerInAttackRange && playerInSightRange && (normalAttacksCount>=3))
        {

            GetComponent<Animator>().SetBool("isAttacking2", true);
            PerformSpecialAttackAndRetreat();
            normalAttacksCount = 0; // Reset the counter

        }*/
        else if (playerInAttackRange && playerInSightRange)
        {

            GetComponent<Animator>().SetBool("isAttacking", true);
            Invoke("AttackPlayer", 1.5f);
            //AttackPlayer();

        }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }


    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);



        if (!alreadyAttacked)
        {
            normalAttacksCount++;
            transform.LookAt(player);
            ///Attack code here
			Invoke("Delay", 1.5f);
            PerformPhysicalAttack();
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

            
             
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void PerformPhysicalAttack()
    {
    
        Player playerHealth = player.GetComponent<Player>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(30); // Adjust the damage value as needed
        }
    }

    IEnumerator PerformSpecialChaseAttack()
    {
        isPerformingSpecialAttack = true;
        Player playerHealth = player.GetComponent<Player>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(30); // Adjust the damage value as needed
        }
        
        

        // Reset animation parameters and speed
        GetComponent<Animator>().SetBool("isAttacking2", false);
       

        // Set back to normal chase
        isPerformingSpecialAttack = false;

        yield return new WaitForSeconds(2f);
        // Continue chasing the player
        
    }

    void PerformSpecialAttackAndRetreat()
    {
        // Initiate special attack
        StartCoroutine(PerformSpecialChaseAttack());

        // Wait for the special attack to finish
        

        // Retreat past the player
        agent.SetDestination(player.position + player.forward * 10f); // Adjust the distance as needed

        // Wait for a short duration before returning to chase state
        

        // Resume chasing the player
        ChasePlayer();
    }
}




