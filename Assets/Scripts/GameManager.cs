using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Ork Dessertboss;
    public Guardian Forestboss;
    public DragonBoss Iceboss;

    public GameObject DessertOrk;
    public GameObject ForestWarrior;
    public GameObject IceDragon;
    public GameObject ForestWall;
    public GameObject IceWall;
    //public GameObject DessertTeleporter;
    //public GameObject TutorialArena;
    public GameObject DessertArena;
    public GameObject ForestArena;
    public GameObject IceArena;
    public GameObject FinalArena;


    //public GameObject DessertTeleporter;
    public GameObject ForestTeleporter;
    public GameObject IceTeleporter;
    public GameObject FinalTeleporter;

    public int arena;
    //1 = dessert, 2 = forest, 3 = ice, 4 =final

    private bool orkDefeated = false;
    private bool centaurDefeated = false;
    private bool dragonDefeated = false;

    void Start()
    {

        if (player == null || Dessertboss == null || ForestTeleporter == null)
        {
            Debug.LogError("Player, Boss, or Next Arena Position not set in the GameManager!");
        }
    }

    void Update()
    {
        
        if (arena == 1)
        {
            if(player.bossKeys >= 2)
            {
                DessertOrk.SetActive(true);
                OrkDefeated();


            }
            
        }
        if (arena == 2)
        {
            if (player.bossKeys >= 2)
            {
                ForestWarrior.SetActive(true);
                ForestWall.SetActive(false);
                CentaurDefeated();

            }
            
        }
        if (arena == 3)
        {
            if (player.bossKeys >= 2)
            {
                IceDragon.SetActive(true);
                IceWall.SetActive(false);
                DragonDefeated();

            }
            
        }


        
        
        
    }

    public void isOrkDefeated()
    {
        if (Dessertboss.Defeated)
        {
            
            orkDefeated = true;
            player.bossKeys = 0;
        }
    }
    public void OrkDefeated()
    {
        isOrkDefeated();
        if (orkDefeated)
        {
            ForestArena.SetActive(true);
            ForestTeleporter.SetActive(true);
            orkDefeated = false;
            if (arena != 1)
            {
                DessertArena.SetActive(false);
            }
        }
    }


    public void CentaurDefeated(){
        isCentaurDefeated();
        if (centaurDefeated)
        {
            
            IceArena.SetActive(true);
            IceTeleporter.SetActive(true);
            centaurDefeated = false;
            if (arena != 2)
            {
                ForestArena.SetActive(false);
            }
        }
    }

    public void isCentaurDefeated()
    {
        if (Forestboss.Defeated)
        {
           
            centaurDefeated = true;
            player.bossKeys = 0;
        }
    }


    public void DragonDefeated()
    {
        isDragonDefeated();
        if (dragonDefeated)
        {

            FinalArena.SetActive(true);
            FinalTeleporter.SetActive(true);
            dragonDefeated = false;
            if (arena != 3)
            {
                IceArena.SetActive(false);
            }
        }
    }

    public void isDragonDefeated()
    {
        if (Iceboss.Defeated)
        {
            

            dragonDefeated = true;
            player.bossKeys = 0;
        }
        
    }



}
