using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Beast : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;
    public Player Myplayer;




    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    [SerializeField] public float initalSpeed = 10f;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
   
    [SerializeField] public int damageAmount = 25; // Adjust this as needed
    

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = true; // Enable off-mesh link traversal
    }
    public List<PlayerCabinTrigger> cabinTriggers = new List<PlayerCabinTrigger>();


    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // Check if the player is inside the cabin
        

        if (!IsPlayerInsideAnyCabin())
        {
            // Player is not inside the cabin, continue with the regular behavior
            if (!playerInSightRange && !playerInAttackRange)
            {
                GetComponent<Animator>().SetBool("isPatrolling", true);
                GetComponent<Animator>().SetBool("isAttacking", false);
                GetComponent<Animator>().SetBool("isChasing", false);
                Patroling();
            }
            else if (playerInSightRange && !playerInAttackRange)
            {
                GetComponent<Animator>().SetBool("isChasing", true);
                GetComponent<Animator>().SetBool("isPatrolling", false);
                GetComponent<Animator>().SetBool("isAttacking", false);
                ChasePlayer();
            }
            else if (playerInAttackRange && playerInSightRange)
            {
                
                GetComponent<Animator>().SetBool("isAttacking", true);
                AttackPlayer();
            }
        }
        else
        {
            // Player is inside the cabin, so the beast remains in patrolling mode
            GetComponent<Animator>().SetBool("isPatrolling", true);
            GetComponent<Animator>().SetBool("isChasing", false);
            GetComponent<Animator>().SetBool("isAttacking", false);
            Patroling();
        }
    }


    private void Patroling()
    {
        agent.speed = initalSpeed;
       
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        
        agent.speed = initalSpeed * 1.5f;
        agent.SetDestination(player.position);
        //agent.speed = initalSpeed / 2;
    }

    private void AttackPlayer()
    {
       
        // Make sure enemy doesn't move
        agent.SetDestination(transform.position);

       // 

        if (!alreadyAttacked)
        {
            transform.LookAt(player);
            // Attack code here
            //Rigidbody rb = Instantiate(projectile, bulletSpawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            //rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            //rb.AddForce(transform.up * 5f, ForceMode.Impulse);
            // Get the PlayerHealth script from the player GameObject
            

            // Check if the playerHealth component is found
            if (Myplayer != null)
            {
                // Apply damage to the player
                Myplayer.TakeDamage(damageAmount);
            }

            // End of attack code

            alreadyAttacked = true;
            
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        //GetComponent<Animator>().SetBool("isAttacking", false);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    

    private bool IsPlayerInsideAnyCabin()
    {
        foreach (var cabinTrigger in cabinTriggers)
        {
            if (cabinTrigger.IsInsideCabin)
            {
                return true;
            }
        }
        return false;
    }

    
}
