using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : MonoBehaviour
{
    public Player player;
    public string armour = "";
    public bool isPoisonArmour;
    public bool isSandArmour;
    public bool isFrostArmour;
    public bool isMovementArmour;
    public float armourHealth;


    // Start is called before the first frame update
    void Start()
    {
        noArmour();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyArmour(string armourType)
    {
        armour = armourType;
        noArmour();
        switch (armourType)
        {
            case "Poison Armour":
                isPoisonArmour = true;
                break;
            case "Sand Armour":
                isSandArmour = true;
                break;
            case "Frost Armour":
                isFrostArmour = true;
                break;
            case "Basic Frost Armour":
                player.freezeProtection = 100;
                break;
            case "Bronze Armour":
                armourHealth = 50;
                break;
            case "Diamond Armour":
                armourHealth = 150;
                break;
            default:
                noArmour();
                break;
        }
    }

    public void noArmour() {

        isPoisonArmour = false;
        isSandArmour = false;
        isFrostArmour = false;
        player.freezeProtection = 10;
        armourHealth = 0;
    }

    public void activatebaseFrostArmour()
    {
        noArmour();
        player.freezeProtection = 100;

    }
    public void activateFrostArmour()
    {
        noArmour();
        isFrostArmour = true;

    }

    public void activatebaseArmour()
    {
        noArmour();
        armourHealth = 50;
    }
    public void activateArmour()
    {
        noArmour();
        armourHealth = 150;
    }
   
    public void activateSandArmour()
    {
        noArmour();
        isSandArmour = true;
    }
    
    public void activatePoisonArmour()
    {
        noArmour();
        isPoisonArmour = true;
    }


    public void deactivateArmour()
    {
        noArmour();
    }
}
