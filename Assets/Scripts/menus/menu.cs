using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void enterGame()
    {
        SceneManager.LoadScene("SampleScene");    
    }

    public void exitGame()
    {
        Debug.Log("Quit is happening");
        Application.Quit();
    }
}
