using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BedInteraction : MonoBehaviour
{
    public Transform interactionPoint;
    public float interactionRadius = 2f;
    public Player player;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteractWithTable();
        }


    }
    private void TryInteractWithTable()
    {
        Collider[] colliders = Physics.OverlapSphere(interactionPoint.position, interactionRadius);

        foreach (var collider in colliders)
        {
            // Check if the player is near the table
            if (collider.CompareTag("Player"))
            {
                player.resetPlayer();
                break; // Exit the loop after finding the player
            }
        }
    }

   
}
