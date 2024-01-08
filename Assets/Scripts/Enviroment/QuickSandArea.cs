using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuicksandArea : MonoBehaviour
{
    public float slowdownRate; // Rate at which the player slows down per second
    private float slowdownAccumulator; // Rate at which the player slows down per second
    public bool playerInQuickSand;
    public float minimumSpeed; // Minimum speed the player can reach
    public float damageRate ; // Damage rate per second
    private float slowdownTimer = 0f; // Timer for slowdown effect
    private float damageTimer = 0f; // Timer for damage effect
    private float timeInSand;
    private float damageInterval = 1.0f; // Interval between damage ticks
    private float initialPlayerSpeed;
    public Player player;
    public Armour armour;
    public Player_Controller PC;

    private void Awake()
    {
        slowdownAccumulator = slowdownRate;
        playerInQuickSand = false;
        initialPlayerSpeed = PC.moveSpeed;
    }
    private void Update()
    {
        if (!armour.isSandArmour)
        {
            if (playerInQuickSand)
            {
                // Slow down the player over time
                SlowdownPlayer();

                // Check if the player has been in the area for 10 seconds
                if (slowdownTimer >= 25.0f)
                {
                    // Apply damage to the player
                    DamagePlayer();
                }
            }
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            if (!armour.isSandArmour)
            {
                player = other.GetComponent<Player>();
                if (player != null)
                {
                    // Start the slowdown effect
                    slowdownTimer = 0f;
                    damageTimer = 0f;
                    timeInSand = 0f;
                    playerInQuickSand = true;
                    player.isSlowed = true;
                }
            }
        }
    }

    

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!armour.isSandArmour)
            {
                if (player != null)
                {
                    // Reset the slowdown effect when the player exits the area
                    slowdownTimer = 0f;
                    damageTimer = 0f;
                    timeInSand = 0f;
                    playerInQuickSand = false;
                    player.resetSpeed(); // Reset player speed to normal
                    player.isSlowed = false;
                }
            }
        }
    }

    private void SlowdownPlayer()
    {
        // Increase the slowdown timer
        slowdownTimer += Time.deltaTime;
       

        // Slow down the player's movement speed over time
        float newSpeed = Mathf.Max(player.getSpeed() - (slowdownRate*slowdownTimer), minimumSpeed);
        player.setSpeed(newSpeed);
        slowdownAccumulator += slowdownRate;
        timeInSand++;
    }

    private void DamagePlayer()
    {
        // Increase the damage timer
        damageTimer += Time.deltaTime;

        // Check if the damage interval has passed
        if (damageTimer >= damageInterval)
        {
            // Reset the timer
            damageTimer = 0f;

            // Apply damage to the player
            player.TakeDamage((int)damageRate);
        }
    }
}
