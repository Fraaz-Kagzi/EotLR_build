using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string inventorySceneName = "Shop"; // Name of your Inventory scene

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            // Load the Inventory scene when 'I' is pressed.
            SceneManager.LoadScene(inventorySceneName);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
