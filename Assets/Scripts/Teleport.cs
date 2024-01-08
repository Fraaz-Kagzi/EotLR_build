using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameManager gm;
    public int nextArena;
    public Transform teleport; // Reference to the Forest Arena transform

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Teleport the player to the forestArena position
            other.transform.position = teleport.position;
            gm.arena = nextArena;
            Debug.Log(other.transform.position);
        }
    }
}
