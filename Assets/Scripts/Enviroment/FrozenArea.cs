using System.Collections;
using System.Collections.Generic;
using UnityEngine;







public class FrozenArea : MonoBehaviour
{
    public float frostIncreaseRate = 0.1f;
    public float frostDecreaseRate = 0.05f;
    public float maxFrostAmount = 1.0f;
    public float minFrostAmount = 0.0f;
    public float freezeProtectionDecreaseRate = 10.0f;
    public int damageRate = 20;
    private float maxFreezeProtection;

    public bool playerInFrozenArea;
    public FrostEffect frostEffect;
    public Player player;

    private float damageTimer = 0f;
    private float damageInterval = 2f; // Adjust this interval as needed

    
    private void Awake()
    {
        playerInFrozenArea = false;
        maxFreezeProtection = player.freezeProtection;
    }
    private void Update()
    {
        Debug.Log(player.freezeProtection);
        // Check if the player is currently in the frozen area
        if (IsPlayerInFrozenArea())
        {
            Debug.Log("in frozen area");
            // Decrease freeze protection over time
            DecreaseFreezeProtection();

            // Increase frost amount over time
            

            // Check if freeze protection has reached zero
            if (player.freezeProtection < 0)
            {
                IncreaseFrost();
                if (frostEffect.FrostAmount >= 0.5)
                {
                    DamagePlayer();
                }
            }
           
 
        }
        else
        {
             Debug.Log("not in frozen area");
            // Decrease frost amount over time
            DecreaseFrost();
            //player.freezeProtection = Mathf.Max(player.freezeProtection, 0);
            IncreaseFreezeProtection();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("entered frost");
            playerInFrozenArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("exited frost");
            
            // DecreaseFrost();
            playerInFrozenArea = false;
        }
    }

    private bool IsPlayerInFrozenArea()
    {
        

        return playerInFrozenArea; 
    }

    private void IncreaseFrost()
    {
        Debug.Log("Increasing Frost");
        // Increase frost amount over time
        float newFrostAmount = Mathf.Clamp01((frostIncreaseRate * Time.deltaTime) + frostEffect.FrostAmount);
        frostEffect.FrostAmount = Mathf.Min(newFrostAmount, maxFrostAmount);
        Debug.Log(frostEffect.FrostAmount);
    }
    
    private void DecreaseFrost()
    {
        Debug.Log("Decreasing Frost");
        // Decrease frost amount over time
        float newFrostAmount = Mathf.Clamp01(frostEffect.FrostAmount - (frostDecreaseRate* Time.deltaTime));
        frostEffect.FrostAmount = Mathf.Max(newFrostAmount, minFrostAmount);
        //Debug.Log("melting");
    }

    private void IncreaseFreezeProtection()
    {
        Debug.Log("Increasing Freeze Protection");
       // Debug.Log(player.freezeProtection);
        // Decrease freeze protection over time
        player.freezeProtection += Time.deltaTime * (freezeProtectionDecreaseRate/2);

        // Ensure freeze protection doesn't go below -1
        player.freezeProtection = Mathf.Min(player.freezeProtection, maxFreezeProtection);
    }

    private void DecreaseFreezeProtection()
    {
        Debug.Log("Decreasing FreezeProtection");
        //Debug.Log(player.freezeProtection);
        // Decrease freeze protection over time
        player.freezeProtection -= Time.deltaTime * freezeProtectionDecreaseRate;

        // Ensure freeze protection doesn't go below -1
        player.freezeProtection = Mathf.Max(player.freezeProtection, -1);
    }

    private void DamagePlayer()
    {
        // Update the damage timer
        damageTimer += Time.deltaTime;

        // Check if the damage interval has passed
        if (damageTimer >= damageInterval)
        {
            // Reset the timer
            damageTimer = 0f;

            // Apply damage to the player
            player.TakeDamage(damageRate);
        }

        if(player.currentHealth < 0)
        {
            player.freezeProtection = maxFreezeProtection;
            frostEffect.FrostAmount = 0;
        }
    }
}

