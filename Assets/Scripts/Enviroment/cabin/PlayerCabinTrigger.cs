using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerCabinTrigger : MonoBehaviour
{
    private bool isInsideCabin = false;
    public Transform respawnPoint;
    public Canvas canvas;
    public GameObject HelpText;
    public GameObject respawnText; 

    public bool IsInsideCabin => isInsideCabin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInsideCabin = true;
            CheckpointManager.SetRespawnPoint(respawnPoint);
            respawnText.SetActive(true);
            HelpText.SetActive(true);
            Invoke("textOff", 3f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInsideCabin = false;
            HelpText.SetActive(false);
        }
    }

    void textOff()
    {
        respawnText.SetActive(false);
    }
}

