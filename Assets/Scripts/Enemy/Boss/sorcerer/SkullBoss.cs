using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.AI;

public class SkullBoss : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    private Shadowmancer shadowScript;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;
    public float bulletSpeed; // Adjust this value as needed





    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    bool alreadyLongAttacked;
    bool alreadySpawned;
    public GameObject OrgeBoss;

    public GameObject projectile;
    public GameObject LongProjectile;
    public int normalAttackCount = 0; // Counter for normal attacks
    public int LongAttackCount = 0; // Counter for long attacks
    public int spawnAttackCount = 0;
    public Transform bulletSpawnPoint;

    //States
    public float sightRange, attackRange, physicalRange;
    public bool playerInSightRange, playerInAttackRange, playerInPhysicalRange;

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
        playerInPhysicalRange = Physics.CheckSphere(transform.position, physicalRange, whatIsPlayer);

        if (shadowScript != null && shadowScript.health <= 0)
        {

            playerInAttackRange = true;
            playerInSightRange = false;
            agent.SetDestination(transform.position);
            Debug.Log("dead");
        }

        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            // Enemy has reached its destination, reset walkPointSet
            walkPointSet = false;
        }



        if (!IsPlayerInsideAnyCabin())
        {
            // Player is not inside the cabin, continue with the regular behavior
            if (!playerInSightRange && !playerInAttackRange)
            {
                GetComponent<Animator>().SetBool("isPhysical", false);
                GetComponent<Animator>().SetBool("isAttacking", false);
                GetComponent<Animator>().SetBool("longAttack", false);
                GetComponent<Animator>().SetBool("isMoving", true);
                Patroling();
            }

            else if (playerInSightRange && !playerInAttackRange && (LongAttackCount >= 3))
            {
                GetComponent<Animator>().SetBool("isPhysical", false);
                GetComponent<Animator>().SetBool("isAttacking", false);
                GetComponent<Animator>().SetBool("longAttack", false);
                GetComponent<Animator>().SetBool("isMoving", true);
                GetComponent<Animator>().SetBool("spawn", true);
                Invoke("SpawnAttack", 4f);
                //SpawnAttack();
            }
            else if (playerInSightRange && !playerInAttackRange)
            {
                GetComponent<Animator>().SetBool("isPhysical", false);
                GetComponent<Animator>().SetBool("isAttacking", false);
                GetComponent<Animator>().SetBool("longAttack", false);
                GetComponent<Animator>().SetBool("isMoving", true);

                ChasePlayer();
            }
            else if (playerInAttackRange && playerInSightRange && !playerInPhysicalRange && (normalAttackCount >= 12))
            {
                
                GetComponent<Animator>().SetBool("longAttack", true);
                
                SpecialAttack();
                

            }
            else if (playerInAttackRange && playerInSightRange && !playerInPhysicalRange)
            {
                //GetComponent<Animator>().SetBool("longAttack", false);
                GetComponent<Animator>().SetBool("isAttacking", true);
                GetComponent<Animator>().SetBool("isPhysical", false);
                AttackPlayer();
            }

            else if (playerInAttackRange && playerInSightRange && playerInPhysicalRange)
            {
                GetComponent<Animator>().SetBool("isPhysical", true);
                CloseAttackPlayer();
            }

         
        }
        else
        {
            // Player is inside the cabin, so the beast remains in patrolling mode
            Patroling();
        }
    }


    private void Patroling()
    {
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

        agent.SetDestination(player.position);
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
            Rigidbody rb = Instantiate(projectile, bulletSpawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(-transform.right * 8f, ForceMode.Impulse);
            rb.AddForce(-transform.up * 10f, ForceMode.Impulse);
            rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);

            //rb.AddForce(transform.left * 1f, ForceMode.Impulse);
            //rb.AddForce(transform.down * 1f, ForceMode.Impulse);



            // End of attack code

            normalAttackCount++;
            Debug.Log(normalAttackCount);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void CloseAttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);



        if (!alreadyAttacked)
        {
            transform.LookAt(player);
            ///Attack code here
			Invoke("Delay", 1.5f);
            PerformPhysicalAttack();
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
   

    void PerformPhysicalAttack()
    {
        Player playerHealth = player.GetComponent<Player>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(15); 
        }
    }


    private void SpecialAttack()
    {
        Invoke("LongAttack", 3.2f);
        // Make sure enemy doesn't move
        
        
       
        
    }

    private void SpawnAttack()
    {

        // Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        if (!alreadySpawned)
        {
            // Instantiate the AI object
            GameObject spawnedOrge = Instantiate(OrgeBoss, transform.position, Quaternion.identity);

            // Activate the AI object (set it visible)
            spawnedOrge.SetActive(true);

            // Implement any other special attack logic here
            Debug.Log("Spawn Attack!");

            LongAttackCount = 0; // Reset the counter after performing the special attack
            alreadySpawned = true;
            Invoke(nameof(ResetAlreadySpawned), 10f);

            GetComponent<Animator>().SetBool("spawn", false);
        }
    }



    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void ResetLongAttack()
    {
        alreadyLongAttacked = false;
    }
    private void ResetAlreadySpawned ()
    {
        alreadySpawned = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    // Method to check if the player is inside any cabin
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

    void LongAttack()
    {
        agent.SetDestination(transform.position);

        //
        if (!alreadyLongAttacked)
        {
            transform.LookAt(player);
            // Attack code here
            Rigidbody rb = Instantiate(LongProjectile, bulletSpawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(-transform.right * 6f, ForceMode.Impulse);
            rb.AddForce(-transform.up * 5f, ForceMode.Impulse);
            rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
            Debug.Log("big attack");

            //rb.AddForce(transform.left * 1f, ForceMode.Impulse);
            //rb.AddForce(transform.down * 1f, ForceMode.Impulse);



            // End of attack code

            LongAttackCount++;
            Debug.Log("Special Attack!");
            alreadyLongAttacked = true;
            Invoke(nameof(ResetLongAttack), 5f);
            // Implement your special attack logic here
            // This can include a unique animation, effects, sound, etc.
            normalAttackCount = 0; // Reset the counter after performing the special attack
            GetComponent<Animator>().SetBool("longAttack", false);

        }

    }

    void DelayedAction()
    {
        // Your code or action here
        
        Debug.Log("Delayed action executed!");
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
