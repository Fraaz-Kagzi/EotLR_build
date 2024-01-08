using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class respawnMenu : MonoBehaviour
{
    private static CheckpointManager cm;
    private void Start()
    {
        // Make sure the cursor is visible and unlocked when a new scene starts
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void Update()
    {
      
    }

    public void respawnGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
