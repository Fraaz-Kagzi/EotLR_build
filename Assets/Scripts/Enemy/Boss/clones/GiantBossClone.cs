using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GiantBossClone : MonoBehaviour
{
    
public NavMeshAgent agent;

public Transform player;

public LayerMask whatIsGround, whatIsPlayer;

public float health;

bool isPerformingSpecialAttack;

private OrkClone orkScript; // Reference to the Ork script


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

        if(orkScript!=null && orkScript.health <= 0)
        {
            
            playerInAttackRange = true;
            playerInSightRange = false;
            agent.SetDestination(transform.position);
            Debug.Log("dead");
        }

        if (!playerInSightRange && !playerInAttackRange) {

            GetComponent<Animator>().SetBool("isAttacking", false);
            GetComponent<Animator>().SetBool("isMoving", true);
            Patroling();
            

        }
        if (playerInSightRange && !playerInAttackRange)
        {
            GetComponent<Animator>().SetBool("isAttacking", false);
            GetComponent<Animator>().SetBool("isMoving", true);

            // 1/3 chance for a special chase attack
            if (Random.Range(0f, 1f) < 1f / 1000f && !isPerformingSpecialAttack)
            {
                StartCoroutine(PerformSpecialChaseAttack());
            }
            else
            {
                ChasePlayer();
            }
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
    IEnumerator PerformSpecialChaseAttack()
    {
        isPerformingSpecialAttack = true;

        // Set special attack animation parameters
        GetComponent<Animator>().SetBool("isAttacking2", true);

        // Increase chase speed (you may need to adjust this value)
        agent.speed *= 1.5f;

        float elapsedTime = 0f;

        while (elapsedTime < 2.5f) // Run the special attack for 2.5 seconds
        {
            // Check if the boss is in contact with the player
            if (Physics.CheckSphere(transform.position, agent.radius + 0.5f, whatIsPlayer))
            {
                // Player is in contact, deal damage
                Player playerHealth = player.GetComponent<Player>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(10);
                }
            }

            // Increase elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Reset animation parameters and speed
        GetComponent<Animator>().SetBool("isAttacking2", false);
        agent.speed /= 1.5f;

        // Set back to normal chase
        isPerformingSpecialAttack = false;

        // Continue chasing the player
        ChasePlayer();
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
}