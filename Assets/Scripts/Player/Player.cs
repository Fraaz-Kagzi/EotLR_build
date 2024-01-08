using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class Player : MonoBehaviour
{
    public static int maxHealth = 250;
    //public CoinManager coinManager;

    public float initialSpeed;
    public  int realmGold;
    public  int stamina = 100;
    public int maxStam;
    public  float freezeProtection = 100;
    public float hunger;
    public bool interacting;
    public CoinManager coinManager;
   
    public bool isSlowed;
    public bool inGameScene = true;
    public bool isPoisoned;
    public  int currentHealth;
    public int bossKeys = 0;

    //UI
    public Healthbar healthBar; // Reference to the health bar script
    public Hunger Hungry;
    public HungerBar hb;
    public Stambar stambar;
    
    

    public TextMeshProUGUI text;




    //currently equipped weapon
    public Player_Controller pc;
    public GameObject equippedWeapon;
    private WeaponManager weaponManagerInstance;

    

    private void Start()
    {
        Transform respawnPoint = CheckpointManager.GetRespawnPoint();
        Debug.Log(respawnPoint);
        weaponManagerInstance = WeaponManager.Instance;
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;
        }
        initialSpeed = pc.moveSpeed;
        int coins = CoinManager.playerCoins;
        realmGold=coins;
        maxStam = stamina;
        currentHealth = maxHealth;
        isSlowed = false;
        isPoisoned = false;
    }
    private void Update(){
        text.SetText("RealmGold: §"+ realmGold);
        CheckForFoodPickup();
      
       
    }

    //health
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        Debug.Log(currentHealth);


        // Check for player death
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        InventoryManager.resetPurchaseItems();
        weaponManagerInstance.resetGun();
        weaponManagerInstance.LoadWeapon("");
        Transform respawnPoint = CheckpointManager.GetRespawnPoint();
        //Debug.Log(respawnPoint);
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;
        }
        else{SceneManager.LoadScene("DeathScene");}
        resetPlayer();
    }

    public void addRealmGold(int value){
        CoinManager.addCoins(value);
        int coins = CoinManager.playerCoins;
        realmGold=coins;
    }

    private void CheckForFoodPickup()
    {
        // Replace with your logic for detecting and picking up food items
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.5f);

            foreach (Collider collider in hitColliders)
            {
                if (collider.CompareTag("Food"))
                {
                    FoodItem foodItem = collider.GetComponent<FoodItem>();
                    if (foodItem != null)
                    {
                        Hungry.EatFood(foodItem);
                    }

                    // Destroy the food item after it's eaten
                    Destroy(collider.gameObject);
                }
            }
        }
    }


    public  int getHealth()
    {
        return currentHealth;
    }

    public void resetPlayer()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, currentHealth);
        //stamina = maxStam;
        //stambar.resetStam();
        Hungry.resetHunger();
        hunger = 100;
        
        hb.SetHunger(100, 100);
    }

    public void setSpeed(float speed)
    {
        pc.normalMoveSpeed = speed;
        Debug.Log("set " + pc.normalMoveSpeed);
    }
    public float getSpeed()
    {
        return pc.normalMoveSpeed;
    }
    public void resetSpeed()
    {
        pc.normalMoveSpeed = initialSpeed;
        Debug.Log("reset " + pc.normalMoveSpeed);
    }



}

