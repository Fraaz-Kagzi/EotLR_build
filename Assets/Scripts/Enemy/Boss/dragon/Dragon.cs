using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;
    public Transform dragonHead;

    public float health;

    //bool isPerformingSpecialAttack;

    private Ork orkScript; // Reference to the Ork script


    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    

    //Attacking
    public float timeBetweenAttacks;
    public float timeBetweenAirAttacks;
    public float numAirAttacks;
    bool alreadyAttacked;
    public float bulletSpeed; // Adjust this value as needed
    public GameObject projectile;
    public Transform bulletSpawnPoint;
    public int normalAttackCount = 0; // Counter for normal attacks


    //States
    public float sightRange, attackRange,landingRange, airAttackRange; // Set your desired landing range here
    public bool playerInSightRange, playerInAttackRange,playerInGroundRange,playerInAirAttackRange;



    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        transform.LookAt(player);
    }

    private void Start()
    {
        Invoke("startSpawn", 2f);
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInGroundRange = Physics.CheckSphere(transform.position, landingRange, whatIsPlayer);
        playerInAirAttackRange = Physics.CheckSphere(transform.position, airAttackRange, whatIsPlayer);

        if (orkScript != null && orkScript.health <= 0)
        {

            playerInAttackRange = true;
            playerInSightRange = false;
            agent.SetDestination(transform.position);
            Debug.Log("dead");
        }
        if (playerInGroundRange)
        {
           
            GetComponent<Animator>().SetBool("isLanding", true);
            //GetComponent<Animator>().SetBool("isMoving", true);
            RotateHeadTowardsPlayer();


        }
        else
        {
            GetComponent<Animator>().SetBool("isLanding", false);
        }
   
        if (!playerInSightRange && !playerInAttackRange)
        {

            GetComponent<Animator>().SetBool("isAttacking", false);
            GetComponent<Animator>().SetBool("isAirAttacking", false);
            GetComponent<Animator>().SetBool("isMoving", true);
            Patroling();


        }
        if (playerInSightRange && !playerInAttackRange)
        {
            GetComponent<Animator>().SetBool("isAttacking", false);
            GetComponent<Animator>().SetBool("isAirAttacking", false);
            GetComponent<Animator>().SetBool("isMoving", true);

            
           
            ChasePlayer();
            
        }
        if (playerInSightRange && playerInAirAttackRange && !playerInGroundRange && normalAttackCount < numAirAttacks)
        {
            GetComponent<Animator>().SetBool("isAirAttacking", true);
            AirAttack();
        }
    
        if (playerInAttackRange && playerInSightRange)
        {

            GetComponent<Animator>().SetBool("isAttacking", true);
            AttackPlayer();




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
            transform.LookAt(player);
            RotateHeadTowardsPlayer(); // Rotate the head before performing the attack
            ///Attack code here
			Invoke("Delay", 1.5f);
            PerformPhysicalAttack();
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void AirAttack()
    {
        // Make sure enemy doesn't move
       agent.SetDestination(transform.position);

        //

        if (!alreadyAttacked)
        {
            transform.LookAt(player);
            // Attack code here
            Rigidbody rb = Instantiate(projectile, bulletSpawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(-transform.right * 6f, ForceMode.Impulse);
            rb.AddForce(-transform.up * 5f, ForceMode.Impulse);
            rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);

            //rb.AddForce(transform.left * 1f, ForceMode.Impulse);
            //rb.AddForce(transform.down * 1f, ForceMode.Impulse);



            // End of attack code

            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAirAttacks);
            normalAttackCount++;
            Debug.Log(normalAttackCount);
            if (normalAttackCount >= numAirAttacks)
            {
                Debug.Log("resetting");
                Invoke("ResetAirAttack", 3f);
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void ResetAirAttack()
    {
        normalAttackCount = 0;
    }

    void PerformPhysicalAttack()
    {
        // For a giant boss, a simple melee attack animation or effect could be triggered here
        // You can also add damage calculation and other effects

        // Example: Set the "isAttacking" parameter in the Animator


        // Example: Deal damage to the player (assuming the player has a health component)
        Player playerHealth = player.GetComponent<Player>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(15); // Adjust the damage value as needed
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);


    }
    void startSpawn()
    {
        Vector3 startDest = new Vector3(-55, -95, 0);
        agent.SetDestination(startDest);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void RotateHeadTowardsPlayer()
    {
        // Calculate the target rotation with a slight downward angle along the y-axis
        Quaternion targetRotation = Quaternion.Euler(10f, 0f, 0f);

        // Smoothly interpolate between the current rotation and the target rotation
        dragonHead.rotation = Quaternion.Slerp(dragonHead.rotation, targetRotation, Time.deltaTime * 1);
        // Adjust the 'rotationSpeed' variable based on how fast you want the head to rotate
    }

}