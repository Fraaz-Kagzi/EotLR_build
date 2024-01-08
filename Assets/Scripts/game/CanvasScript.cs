using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CanvasScript : MonoBehaviour
{
    public Canvas canvas;
    public Player player;
    public GameObject freezingGameObject; // Assign the GameObject in the Inspector
    public GameObject slowedGameObject;
    public GameObject poisonedGameObject;
    public GameObject hungerGameObject;

    // Update is called once per frame
    void Update()
    {
        if (player != null && freezingGameObject != null)
        {
            if (player.freezeProtection <= 0)
            {
                // Activate the sprite GameObject
                freezingGameObject.SetActive(true);
            }
            else
            {
                // Deactivate the sprite GameObject
                freezingGameObject.SetActive(false);
            }
        }
        if (player != null && slowedGameObject != null)
        {
            if (player.isSlowed == true)
            {
                // Activate the sprite GameObject
                slowedGameObject.SetActive(true);
            }
            else
            {
                // Deactivate the sprite GameObject
                slowedGameObject.SetActive(false);
            }
        }
        if (player != null && poisonedGameObject != null)
        {
            if (player.isPoisoned == true)
            {
                // Activate the sprite GameObject
                poisonedGameObject.SetActive(true);
            }
            else
            {
                // Deactivate the sprite GameObject
                poisonedGameObject.SetActive(false);
            }
        }
        if (player != null && hungerGameObject != null)
        {
            if (player.hunger <= 0)
            {
                // Activate the sprite GameObject
                hungerGameObject.SetActive(true);
            }
            else
            {
                // Deactivate the sprite GameObject
                hungerGameObject.SetActive(false);
            }
        }
    }
}

