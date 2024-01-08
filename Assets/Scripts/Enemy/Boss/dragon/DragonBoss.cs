using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this for UI functionality
using TMPro; // Add this for TextMeshPro functionality



public class DragonBoss : MonoBehaviour
{
    public CoinManager coinManager;
    public Player player;

    //enemy stats
    public int health = 1000;
    public int maxHealth = 1000;
    public bool Defeated = false;


    // value of each enemy CoinManager
    public int value;

    // UI elements
    public Canvas bossCanvas;
    public TextMeshProUGUI bossTitleText;

    public bossHealthBar healthBar; // Reference to the health bar script


    private void Awake()
    {
        healthBar = GetComponentInChildren<bossHealthBar>();

        // Assume that the bossCanvas and bossTitleText are child objects of the boss GameObject
        bossCanvas = GetComponentInChildren<Canvas>();
        bossTitleText = GetComponentInChildren<TextMeshProUGUI>();

        // Initially hide the UI elements
        SetUIVisibility(false);
    }
    private void Start()
    {

        health = maxHealth;
    }





    // Called when the enemy is shot
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        Debug.Log(health);

        if (health <= 0)
        {
            GetComponent<Animator>().SetBool("isDead", true);
            //healthBar.DeactivateHealthBar();
            Debug.Log("Enemy is destroyed!");

            // Deactivate the enemy after a delay of 10 seconds
            Invoke("DeactivateEnemy", 10f);

            // Re-enable the enemy after a delay
            //coinManager.addCoins(value);
            player.addRealmGold(value);

        }

        // ...



        Debug.Log(player.realmGold);

    }

    private void DeactivateEnemy()
    {
        // Disable physics if the object has a Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero; // Set velocity to zero
            rb.isKinematic = true;
            rb.freezeRotation = true; // Optionally freeze rotation
            rb.Sleep(); // Put rigidbody to sleep
        }

        // Deactivate the enemy GameObject
        Defeated = true;
        gameObject.SetActive(false);






    }


    private void Update()
    {
        // Check the distance between the player and the boss
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Show/hide UI elements based on distance
        SetUIVisibility(distanceToPlayer <= 150f);
    }

    private void SetUIVisibility(bool isVisible)
    {
        // Set the visibility of the bossCanvas and bossTitleText
        if (bossCanvas != null)
        {
            bossCanvas.enabled = isVisible;
        }

        if (bossTitleText != null)
        {
            bossTitleText.enabled = isVisible;
        }
    }




}

