using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   public CoinManager coinManager;
    public Player player;
    //public EnemySpawner spawner; // Reference to the spawner
    public GameObject itemToDropPrefab;
    //enemy stats
    public int health;
    public int maxHealth;

    // value of each enemy CoinManager
    public int value;
    
    public EnemyHealthBar healthBar; // Reference to the health bar script

    private void Awake()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
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
          

            Debug.Log("Enemy is destroyed!");
            // Enemy is "destroyed" by deactivating it
            //gameObject.SetActive(false);

            // Re-enable the enemy after a delay
            //coinManager.addCoins(value);
            
            Invoke("delete",0f);
            player.addRealmGold(value);
            if (itemToDropPrefab != null)
            {
                // Instantiate the item at the enemy's position
                GameObject droppedItem = Instantiate(itemToDropPrefab, transform.position, Quaternion.identity);

                
            }

        }
        Debug.Log(player.realmGold);
        
    }

    // Called to respawn the enemy
    void Respawn()
    {
        // Reset health and reactivate the enemy
        health = 100;
        healthBar.SetHealth(100, 100);
        Debug.Log(health);
        gameObject.SetActive(true);
    }
    void delete()
    {
        gameObject.SetActive(false);
        Debug.Log("enemy killed");
        //spawner.EnemyKilled();
        
    }
}
