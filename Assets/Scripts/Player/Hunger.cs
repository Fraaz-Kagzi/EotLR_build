using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Hunger : MonoBehaviour
{
    public Player player;
    public float maxHunger;
    public int currentHunger;
    public int damagePerSecond = 2;

    public float hungerDecreaseRate = 1f; // Decrease by 1 every second
    public float foodPickupRange = 2f; // Range for picking up food items

    public HungerBar hungerBar;
    public Armour armour;


    private void Start()
    {
        maxHunger = player.hunger;
        InvokeRepeating("DecreaseHunger", 1f, 1f);
        hungerBar.SetHunger(maxHunger, maxHunger);
    }

    private void Update()
    {
        //CheckForFoodPickup();
        //CheckPlayerStatus();
    }

    private void DecreaseHunger()
    {
        player.hunger = Mathf.Max(0, player.hunger - 1);
        hungerBar.UpdateBar(player.hunger, maxHunger);

        if (player.hunger == 0)
        {
            player.TakeDamage(1);
        }
    }

    

    

    public void EatFood(FoodItem foodItem)
    {
        // Increase the hunger bar by the value of the food item
        player.hunger = Mathf.Min(maxHunger, player.hunger + foodItem.foodValue);
        hungerBar.UpdateBar(player.hunger, maxHunger);

        // Check if the food is poisonous
        if (foodItem.isPoisonous && ! armour.isPoisonArmour)
        {
            player.isPoisoned = true;
            StartCoroutine(ApplyPoisonDamage());
        }
        if (foodItem.isKey)
        {

            addKey();
        }
    }

    private IEnumerator ApplyPoisonDamage()
    {
        int totalPoisonTicks = 5;
        float poisonInterval = 1f; // 1 second interval
        int damagePerTick = 2;

        for (int i = 0; i < totalPoisonTicks; i++)
        {
            // Apply damage per tick
            player.TakeDamage(damagePerTick);

            // Wait for the next tick
            yield return new WaitForSeconds(poisonInterval);
        }
        player.isPoisoned = false;
    }

    private void addKey()
    {
        player.bossKeys = player.bossKeys + 1;
    }


        public void resetHunger()
    {
        player.hunger = maxHunger;
        hungerBar.resetHunger();
    }
}

