using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wonMenu : MonoBehaviour
{
    public void replayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void exitGame()
    {
        Debug.Log("Quit is happening");
        Application.Quit();
    }
}
